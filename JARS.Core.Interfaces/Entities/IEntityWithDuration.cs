using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Entities
{
    public interface IEntityWithDuration : IEntityBase
    {
        double Duration { get; set; }
    }
}
