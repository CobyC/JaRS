using System.Collections.Generic;

namespace JARS.Core.Interfaces.Entities
{
    public interface IJarsUserBase
    {
        int Id { get; set; }
        string UserName { get; set; }
        List<string> Roles { get; set; }
        List<string> Permissions { get; set; }
    }
}
