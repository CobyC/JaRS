using System;
using System.ComponentModel.Composition;
using JARS.Client.Bootstrap;
using JARS.Core;
using JARS.Core.Interfaces.Entities;
using JARS.Core.Interfaces.Processors;
using JARS.Core.WinForms.Interfaces.Plugins;
using JARS.Core.WinForms.Interfaces.Processors;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Test.Processors
{
    [TestClass]
    public class UnitTest1
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



        [TestMethod]
        public void Generate_generic_parameter_type_from_type()
        {
            IEntityBase ent = new StandardAppointment() { Id = 10 };

            IProcessor processor = _factory.GetJarsProcessor<IProcessor>(ent.GetType().Name);
            if (processor is IProcessorForShowAppointmentForm)
                Assert.IsTrue(true);
           
            if (processor is IProcessorForAppointmentEvents)
                Assert.IsTrue(true);

            if (processor is IProcessorForEventServiceCommandReceived)
                Assert.IsTrue(true);
        }      
    }


    public interface IJobProcessBase
    { }

    public interface IJobProcess<T> : IJobProcessBase where T : IEntityBase
    { }

    public class SomeJobProcess : IJobProcess<JarsJob>
    {

    }
}
