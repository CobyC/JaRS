using JARS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JARS.Entities
{
    /// <summary>
    /// This class represents a user account in JaRS.
    /// These are accounts linked to people using he JaRS application.    
    /// </summary>  
    /// 
    [Serializable]
    public class JarsUserHeader : JarsUserBase
    {
        public JarsUserHeader()
        {  }
    }
}
