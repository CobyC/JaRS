using JARS.Core;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using NHibernate;
using System;
using System.ComponentModel.Composition;

namespace JARS.Data.NH.Jars.Repositories
{
    [Export(typeof(IJarsUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class JarsUserRepository : DataRepositoryNhCrudBase<JarsUser>, IJarsUserRepository
    {
        [ImportingConstructor()]
        public JarsUserRepository(IDataContextNhJars DbContext) : base(DbContext)
        { }

        public JarsUser GetByUserName(string adUsernameOrEmail)
        {
            using (ISession s = DBContext.SessionFactory.OpenSession())
            {
                using (ITransaction t = s.BeginTransaction())
                {
                    try
                    {                        
                        JarsUser retUser = null;
                        if (adUsernameOrEmail.Contains("@"))
                            retUser = s.QueryOver<JarsUser>().Where(u => u.Email == adUsernameOrEmail)
                                .SingleOrDefault<JarsUser>();
                        else
                            retUser = s.QueryOver<JarsUser>().Where(u => u.UserName == adUsernameOrEmail)                                
                                .SingleOrDefault<JarsUser>();

                        return retUser;
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        Logger.Error(ex.Message, ex);
                        throw ex;
                    }
                }

            }
        }

        //public JarsUser GetByUserNameEagerly(string adUsernameOrEmail)
        //{
        //    using (ISession s = DBContext.SessionFactory.OpenSession())
        //    {
        //        using (ITransaction t = s.BeginTransaction())
        //        {
        //            try
        //            {
        //                var query = s.QueryOver<JarsUser>()
        //                    .Future();
        //                //add the additional eager loading properties.
        //                s.QueryOver<JarsUser>().Fetch(p => p.Roles).Eager.Future();
        //                s.QueryOver<JarsUser>().Fetch(p => p.Settings).Eager.Future();

        //                if (adUsernameOrEmail.Contains("@"))
        //                    return query.Where(u => u.Email == adUsernameOrEmail).SingleOrDefault();

        //                else
        //                    return query.Where(u => u.UserName == adUsernameOrEmail).SingleOrDefault();

        //            }
        //            catch (Exception ex)
        //            {
        //                t.Rollback();
        //                Logger.Error(ex.Message, ex);
        //                throw ex;
        //            }
        //        }
        //    }
        //}
    }
}
