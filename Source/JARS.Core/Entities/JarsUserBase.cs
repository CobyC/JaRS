using JARS.Core.Interfaces.Entities;
using System.Collections.Generic;

namespace JARS.Core.Entities
{
    public class JarsUserBase : IJarsUserBase
    {
        public JarsUserBase()
        {
            Id = 0;
            UserName = "";
            Roles = new List<string>();
            Permissions = new List<string>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
