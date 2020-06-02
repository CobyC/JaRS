using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Interfaces.Entities
{
    public interface IEntityWithNotes : IEntityBase
    {
        string AddedNotes { get; set; }
    }
}
