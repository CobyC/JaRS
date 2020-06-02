using JARS.BOS.Entities;
using JARS.BOS.WinForms.Plugins.Processors;
using JARS.Client.Bootstrap;
using JARS.Core;
using JARS.Core.Interfaces.Processors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace JARS.Test.BOS.ClientSide.Tests
{
    [TestClass]
    public class ProcessorTests
    {

        [Import]
        IProcessorFactory _factory;

        [TestInitialize]
        public void TestMethod1()
        {
            JarsCore.Container = MEFClientLoader.Init();
            if (JarsCore.Container != null)
                JarsCore.Container.SatisfyImportsOnce(this);
        }

       // private readonly ServiceStackHost appHost;
       // protected GlobalContext Context { get { return GlobalContext.Instance; } }

        //public ProcessorTests()
        //{
        //    //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
        //    //JarsCore.Container = MEFBusinessLoader.Init();
        //    //appHost = new BasicAppHost() { };          
        //    //appHost.Init();
        //    //appHost.Container.AddTransient<BOSEntityService>();
            
        //}

        //[TestCleanup]
        //public void OneTimeTearDown() => appHost.Dispose();

        [TestMethod]
        public async Task TestMethod1Async()
        {
            BOSEntity ent = new BOSEntity() { Id = 10 };
            IProcessor processor = _factory.GetJarsProcessor<IProcessor>(ent.GetType().Name);

            if (processor is BOSEntityAppointmentProcessor proc)
            {
                //BOSEntityAppointmentProcessor proc = new BOSEntityAppointmentProcessor();
                await proc.LoadOrRefreshEntityDataAsync();
                var list = proc.DataList;
            }
        }
    }
}
