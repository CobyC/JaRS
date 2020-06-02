using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Core.Authentication
{
    public class JarsUserClientSettings
    {
        public JarsUserClientSettings()
        {
        }
        public string LastProvider { get; set; }
        public bool LastRememberMe { get; set; }
        public string Salt { get; set; }
        public string Credentials { get; set; }
        
    }
}
