using JARS.Core.Interfaces.Entities;
using System.Collections.Generic;

namespace JARS.SS.DTOs.Interfaces
{
    public interface ICrudNotificationDto<TEntity> : INotificationBaseDto<TEntity> 
        where TEntity : IEntityBase
    {
        //TEntity Entity { get; set; }
        List<TEntity> Entities { get; set; }
    }
}
