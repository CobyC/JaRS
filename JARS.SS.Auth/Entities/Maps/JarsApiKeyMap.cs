using FluentNHibernate.Mapping;

namespace JARS.SS.Auth.Entities.Maps
{
    public class JarsApiKeyMap : ClassMap<JarsApiKey>
    {
        public JarsApiKeyMap()
        {
            Table($"{nameof(JarsApiKey)}s");
            LazyLoad();
            Id(x => x.Id); 
            Map(x => x.UserAuthId);
            Map(x => x.RefId);
            Map(x => x.RefIdStr);
            Map(x => x.CreatedDate);
            Map(x => x.CancelledDate);
            Map(x => x.ExpiryDate);
            Map(x => x.Environment);
            Map(x => x.KeyType);
            Map(x => x.Notes);
        }
    }
}
