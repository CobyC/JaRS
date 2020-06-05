using JARS.BOS.Data;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Data.NH.Interfaces;
using JARS.Data.NH.Jars;
using JARS.Data.NH.Jars.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace JARS.Tests.Data.NH
{
    [TestClass]
    public class NH_DataContext_Tests
    {
        [Import(typeof(IDataContextBOS))]
        IDataContextBaseNh ExternalContext;
        [Import(typeof(IDataContextNhJars))]
        IDataContextBaseNh JarsContext;
        
        ////or just import but by the actual type
        //[Import]
        //IDataContextNhJars jarsActualContext;

        ////try import many
        //[ImportMany]
        //IEnumerable<IDataContextBaseNh> nhBaseList; //<-results in 0

        public NH_DataContext_Tests(IDataContextBaseNh externalContext, IDataContextBaseNh jarsContext, IDataContextBaseNh qlContext)
        {
            ExternalContext = externalContext;
            JarsContext = jarsContext;
           
        }

        [TestInitialize]
        public void Initialize()
        {
            JarsCore.Container = MEFBusinessLoader.Init();//<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]
        }


        [TestMethod]
        public void GetAccessToTheSessionFactoryAndClassMetaData()
        {
            IDictionary<string, NHibernate.Metadata.IClassMetadata> jClassData = JarsContext.SessionFactory.GetAllClassMetadata();
          
            IDictionary<string, NHibernate.Metadata.IClassMetadata> xClassData = ExternalContext.SessionFactory.GetAllClassMetadata();
            Assert.IsTrue(jClassData.Count > 0);
          
            Assert.IsTrue(xClassData.Count > 0);
        }

        [TestMethod]
        public void Does_Contexts_exist_using_mef()
        {
            Assert.IsNotNull(ExternalContext);
            Assert.IsNotNull(JarsContext);
            ////check if the session factory can be created (this will also build the configurations

            Assert.IsFalse(ExternalContext.SessionFactory.IsClosed);
            Assert.IsFalse(JarsContext.SessionFactory.IsClosed);
          
        }


        [TestMethod]
        public void Does_Contexts_exist_not_using_mef()
        {
            //DataContextExternalNh extCon = new DataContextExternalNh();
            //Assert.IsFalse(extCon.SessionFactory.IsClosed);
           
            DataContextNhJars jCon = new DataContextNhJars();
            Assert.IsFalse(jCon.SessionFactory.IsClosed);
        }
    }
}
