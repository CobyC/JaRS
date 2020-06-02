using FluentNHibernate.Mapping;

namespace JARS.BOS.Entities.Maps
{
    public class BOSExternalEntityMap : ClassMap<BOSExternalEntity>
    {
        public BOSExternalEntityMap()
        {
            ReadOnly();
            Table("WorkOrders");
            Id(x=> x.Id).Column("OrderNo");
            Map(x => x.Location).Column("Address");
            Map(x => x.LineOfWork).Column("Trade");
            Map(x => x.Description);
            Map(x => x.Duration).Column("Duration");
        }
    }
}
