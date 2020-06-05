using System;
using System.ComponentModel.Composition;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Client;
using JARS.Core.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Test.WinForms
{
    [TestClass]
    public class TestSimpleSchedForm
    {
        [Import]
        IDataRepositoryFactory _repFactory; //<-- this calls the repository factory that also creates the connection to the database

        public TestSimpleSchedForm(IDataRepositoryFactory repFactory) : base()
        {
            _repFactory = repFactory;
        }

        [TestInitialize]
        public void Initialize()
        {
            JarsCore.Container = MEFBusinessLoader.Init(); //<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]            
        }

        //[TestCleanup]
        //public void OneTimeTearDown() => appHost.Dispose();

        [TestMethod]
        public void LaunchForm()
        {
            GlobalContext context = new GlobalContext();


            SimpleSchedForm frm = new SimpleSchedForm(_repFactory);
            frm.ShowDialog();
        }
    }
}
