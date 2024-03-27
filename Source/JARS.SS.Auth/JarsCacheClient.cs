using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Jars.Interfaces;
using JARS.SS.Auth.Entities;
using JARS.SS.Auth.Entities.Maps;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace JARS.SS.Auth
{
    public class JarsCacheClient : JarsCacheClient<JarsCacheEntry> { }

    public class JarsCacheClient<TEntry> : ICacheClient, IRequiresSchema, ICacheClientExtended, IRemoveByPattern, IDisposable
        where TEntry : ICacheEntry, new()
    {
        [Import]
        internal IDataRepositoryFactory _DataRepositoryFactory;

        private const string MODIEFIED_BY = "JARSCACHE";

        public JarsCacheClient()
        {
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

        public JarsCacheClient(IDataRepositoryFactory dataRepositoryFactory) : this()
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IStringSerializer _StringSerializer;
        public IStringSerializer StringSerializer
        {
            get
            {
                if (_StringSerializer == null)
                    _StringSerializer = new JsvStringSerializer();
                return _StringSerializer;
            }
        }

        private IGenericEntityRepositoryBase<JarsCacheEntry, IDataContextNhJars> _CacheEntityRepository;
        internal IGenericEntityRepositoryBase<JarsCacheEntry, IDataContextNhJars> CacheEntityRepository
        {
            get
            {
                if (_CacheEntityRepository == null)
                {
                    _CacheEntityRepository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsCacheEntry, IDataContextNhJars>>();
                }
                return _CacheEntityRepository;
            }
        }


        TEntry CreateEntry(string id, string data = null, DateTime? created = null, DateTime? expires = null)
        {
            var createdDate = created ?? DateTime.UtcNow;
            return new TEntry
            {
                Id = id,
                Data = data,
                ExpiryDate = expires,
                CreatedDate = createdDate,
                ModifiedDate = createdDate,
            };
        }

        public bool Remove(string key)
        {
            CacheEntityRepository.Delete(key);
            return true;
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            IList<object> keyList = new List<object>();
            keys.Each((x) => { keyList.Add(x); });
            CacheEntityRepository.DeleteListByIds(keyList);
        }

        public T Get<T>(string key)
        {
            TEntry entry = CacheEntityRepository.GetById(key).ConvertTo<TEntry>();
            var cache = Verify(entry);
            return cache == null ? default(T) : StringSerializer.DeserializeFromString<T>(cache.Data);
        }

        public long Increment(string key, uint amount)
        {

            long nextVal;
            //using (NHibernate.ISession session = (CacheEntityRepository.CurrentDataContext as IDataContextNhJars).SessionFactory.OpenSession())
            //{
            //    using (NHibernate.ITransaction transaction = session.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            //    {

            var cache = Verify(Get<TEntry>(key));
            if (cache == null)
            {
                nextVal = amount;
                CacheEntityRepository.CreateUpdate(CreateEntry(key, nextVal.ToString()) as JarsCacheEntry, MODIEFIED_BY);
            }
            else
            {
                nextVal = long.Parse(cache.Data) + amount;
                cache.Data = nextVal.ToString();
                CacheEntityRepository.CreateUpdate(cache as JarsCacheEntry, MODIEFIED_BY);
            }
            //    transaction.Commit();
            //}
            //}
            return nextVal;
        }

        public long Decrement(string key, uint amount)
        {
            long nextVal;
            //using (NHibernate.ISession session = (CacheEntityRepository.CurrentDataContext as IDataContextNhJars).SessionFactory.OpenSession())
            //{
            //    using (NHibernate.ITransaction transaction = session.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            //    {

            var cache = Verify(Get<TEntry>(key));
            if (cache == null)
            {
                nextVal = -amount;
                CacheEntityRepository.CreateUpdate(CreateEntry(key, nextVal.ToString()) as JarsCacheEntry, MODIEFIED_BY);
            }
            else
            {
                nextVal = long.Parse(cache.Data) - amount;
                cache.Data = nextVal.ToString();
                CacheEntityRepository.CreateUpdate(cache as JarsCacheEntry, MODIEFIED_BY);
            }
            //        transaction.Commit();
            //    }
            //}
            return nextVal;
        }

        public bool Add<T>(string key, T value)
        {
            try
            {
                JarsCacheEntry entry = CreateEntry(key, StringSerializer.SerializeToString(value)) as JarsCacheEntry;
                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return false;
            }
        }

        private bool UpdateIfExists<T>(string key, T value)
        {
            bool exists = false;
            var entry = CacheEntityRepository.Where(q => q.Id == key).SingleOrDefault();
            if (entry != null)
            {
                exists = true;
                entry.Data = StringSerializer.SerializeToString(value);
                entry.ModifiedDate = DateTime.UtcNow;

                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
            }
            return exists;
        }

        private bool UpdateIfExists<T>(string key, T value, DateTime expiresAt)
        {
            bool exists = false;
            var entry = CacheEntityRepository.Where(q => q.Id == key).SingleOrDefault();
            if (entry != null)
            {
                exists = true;
                entry.Data = StringSerializer.SerializeToString(value);
                entry.ExpiryDate = expiresAt;
                entry.ModifiedDate = DateTime.UtcNow;

                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
            }
            return exists;
        }

        public bool Set<T>(string key, T value)
        {
            var exists = UpdateIfExists(key, value);

            if (!exists)
            {
                try
                {
                    JarsCacheEntry entry = CreateEntry(key, StringSerializer.SerializeToString(value)) as JarsCacheEntry;
                    CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
                }
                catch (Exception)
                {
                    exists = UpdateIfExists(key, value);
                    if (!exists)
                        throw;
                }
            }
            return true;
        }

        public bool Replace<T>(string key, T value)
        {
            var exist = UpdateIfExists(key, value);
            if (!exist)
            {
                JarsCacheEntry entry = CreateEntry(key, StringSerializer.SerializeToString(value)) as JarsCacheEntry;
                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
            }
            return true;
        }


        public bool Add<T>(string key, T value, DateTime expiresAt)
        {
            try
            {
                JarsCacheEntry entry = CreateEntry(key, StringSerializer.SerializeToString(value), expires: expiresAt) as JarsCacheEntry;
                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
                return false;
            }
        }

        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            var exists = UpdateIfExists(key, value, expiresAt);
            if (!exists)
            {
                try
                {
                    Add(key, value, expiresAt);
                }
                catch (Exception ex)
                {
                    exists = UpdateIfExists(key, value, expiresAt);
                    if (!exists)
                        throw ex;
                }
            }
            return true;
        }

        public bool Replace<T>(string key, T value, DateTime expiresAt)
        {
            var exists = UpdateIfExists(key, value, expiresAt);
            if (!exists)
            {
                Add(key, value, expiresAt);
            }
            return true;
        }

        public bool Add<T>(string key, T value, TimeSpan expiresIn)
        {
            try
            {
                JarsCacheEntry entry = CreateEntry(key, StringSerializer.SerializeToString(value), expires: DateTime.UtcNow.Add(expiresIn)) as JarsCacheEntry;
                CacheEntityRepository.CreateUpdate(entry, MODIEFIED_BY);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
                return false;
            }
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return Set(key, value, expiresAt: DateTime.UtcNow.Add(expiresIn));
        }

        public bool Replace<T>(string key, T value, TimeSpan expiresIn)
        {
            return Replace(key, value, expiresAt: DateTime.UtcNow.Add(expiresIn));
        }
        public void FlushAll()
        {
            IList<JarsCacheEntry> AllList = CacheEntityRepository.GetAll();
            CacheEntityRepository.DeleteList(AllList);
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            //var query = QueryOver.Of<JarsCacheEntry>().WhereRestrictionOn(q => q.Id).IsIn(keys.ToArray());
            //IList<TEntry> AllList = CacheEntityRepository.QueryOverOf(query).ToList() as IList<TEntry>;
            IList<TEntry> AllList = CacheEntityRepository.Where(c => keys.Contains(c.Id)).ToList() as IList<TEntry>;
            var results = Verify(AllList);
            var map = new Dictionary<string, T>();

            results.Each(x =>
                map[x.Id] = StringSerializer.DeserializeFromString<T>(x.Data));

            foreach (var key in keys)
            {
                if (!map.ContainsKey(key))
                    map[key] = default(T);
            }
            return map;
        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
            var rows = values.Select(entry =>
                    CreateEntry(entry.Key, StringSerializer.SerializeToString(entry.Value)))
                    .ToList() as IList<JarsCacheEntry>;

            CacheEntityRepository.CreateUpdateList(rows, MODIEFIED_BY);
        }

        public void InitSchema()
        {
            var currentConfig = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(JarsCore.JarsConnectionString).ShowSql)
                .Mappings(m => m.FluentMappings.Add(typeof(JarsCacheEntryMap)))
                .BuildConfiguration();
            try
            {
                SchemaUpdate upd = new SchemaUpdate(currentConfig);
                upd.Execute(true, true);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
            }
        }

        public List<TEntry> Verify(IList<TEntry> entries)
        {
            List<TEntry> results = entries.ToList();
            var expired = results.RemoveAll(x => x.ExpiryDate != null && DateTime.UtcNow > x.ExpiryDate);
            if (expired > 0)
            {
                IList<JarsCacheEntry> list = CacheEntityRepository.Where(q => DateTime.UtcNow > q.ExpiryDate);
                CacheEntityRepository.DeleteList(list);
            }
            return results;
        }

        public TEntry Verify(TEntry entry)
        {
            if (entry != null && entry.ExpiryDate != null && DateTime.UtcNow > entry.ExpiryDate)
            {
                IList<JarsCacheEntry> list = CacheEntityRepository.Where(q => DateTime.UtcNow > q.ExpiryDate);
                CacheEntityRepository.DeleteList(list);
                return default(TEntry);
            }
            return entry;
        }

        public TimeSpan? GetTimeToLive(string key)
        {
            var cache = CacheEntityRepository.GetById(key);
            if (cache == null)
                return null;

            if (cache.ExpiryDate == null)
                return TimeSpan.MaxValue;

            return cache.ExpiryDate - DateTime.UtcNow;
        }

        public void RemoveByPattern(string pattern)
        {
            var dbPattern = pattern.Replace('*', '%');
            //var query = QueryOver.Of<JarsCacheEntry>().WhereRestrictionOn(q => q.Id).IsLike(dbPattern, MatchMode.Anywhere);
            //List<TEntry> allList = CacheEntityRepository.QueryOverOf(query).ToList().ConvertAll(x => x.ConvertTo<TEntry>());
            List<TEntry> allList = CacheEntityRepository.Where(q=> q.Id.IsLike(dbPattern,MatchMode.Anywhere)).ToList().ConvertAll(x => x.ConvertTo<TEntry>());

            CacheEntityRepository.DeleteList(allList.ConvertAll(x => x.ConvertTo<JarsCacheEntry>()));
        }

        public IEnumerable<string> GetKeysByPattern(string pattern)
        {
            if (pattern == "*")
            {
                var allList = CacheEntityRepository.GetAll();
                IList<string> idList = new List<string>();
                allList.Each((x) => { idList.Add(x.Id); });
                return idList;
            }

            var dbPattern = pattern.Replace('*', '%');

            //var query = QueryOver.Of<JarsCacheEntry>().WhereRestrictionOn(q => q.Id).IsLike(dbPattern, MatchMode.Anywhere);
            //IList<TEntry> allList1 = CacheEntityRepository.QueryOverOf(query).ToList() as IList<TEntry>;
            IList<TEntry> allList1 = CacheEntityRepository.Where(q=> q.Id.IsLike(dbPattern, MatchMode.Anywhere)).ToList() as IList<TEntry>;
            IList<string> idList1 = new List<string>();
            allList1.Each((x) => { idList1.Add(x.Id); });
            return idList1;
        }

        public void RemoveByRegex(string regex)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //if (_CacheEntityRepo != null)
            //    _CacheEntityRepo = null;
        }

        public void RemoveExpiredEntries()
        {
            throw new NotImplementedException();
        }
    }
}
