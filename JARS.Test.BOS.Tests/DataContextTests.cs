using JARS.BOS.Data;
using JARS.BOS.Entities;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;

namespace JARS.Test.BOS.Tests
{
    [TestClass]
    public class DataContextTests
    {
        [TestInitialize]
        public void Initialize()
        {
            //This requires the app config file to contain the [UseWebPath] property
            JarsCore.Container = MEFBusinessLoader.Init();//<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]
        }

        [TestMethod]
        public void Create_Context_Object_Without_Mef()
        {
            DataContextBOS bosContext = new DataContextBOS();
            NHibernate.Cfg.Configuration bos = bosContext.InitializeConnections() as NHibernate.Cfg.Configuration;
            Assert.IsFalse(bosContext.SessionFactory.IsClosed);

            //DataContextExternalBOS bosExContext = new DataContextExternalBOS();
            //NHibernate.Cfg.Configuration bosEx = bosExContext.InitializeConnections() as NHibernate.Cfg.Configuration;
            //Assert.IsFalse(bosExContext.SessionFactory.IsClosed);
        }

        [Import(typeof(IDataContextBOS))]
        IDataContextBaseNh _bosContext;
        //[Import(typeof(IDataContextExternalBOS))]
        //IDataContextBaseNh _bosExternalContext;
        [Import]
        public IDataRepositoryFactory _DataRepositoryFactory;

        [TestMethod]
        public void Create_Context_Object_With_Mef()
        {
            Assert.IsNotNull(_bosContext);
           // Assert.IsNotNull(_bosExternalContext);
        }

        [TestMethod]
        public void Create_Generic_Repository_Objects_With_Mef()
        {

            //IGenericEntityReadOnlyRepositoryBase<BOSExternalEntity, IDataContextExternalBOS> _ro_repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityReadOnlyRepositoryBase<BOSExternalEntity, IDataContextExternalBOS>>();
            //var ro = _ro_repository.GetAll();

            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> _repository = _DataRepositoryFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
            var crud = _repository.CreateUpdate(new BOSEntity(), "TEST");

            Assert.IsNotNull(_bosContext);
            //Assert.IsNotNull(_bosExternalContext);
        }

    }
}
