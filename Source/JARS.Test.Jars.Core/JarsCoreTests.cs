using JARS.Core.Rules.Utils;
using JARS.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JARS.Test.Jars.Core
{
    [TestClass]
    public class JarsCoreTests
    {
        [TestMethod]
        public async Task Test_assembly_Loader_utility_Test()//test neetds to be Task to attach a debugger.
        {
            IList<Type> types = await AssemblyLoaderUtil.FindAllEntityTypesThatAllowRules();
            Assert.IsNotNull(types);
        }
    }
}