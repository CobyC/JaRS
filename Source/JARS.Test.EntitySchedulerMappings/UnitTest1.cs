using System;
using DevExpress.XtraScheduler;
using JARS.Core.Entities;
using JARS.Core.Interfaces;
using JARS.Core.Interfaces.Entities;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JARS.Test.EntitySchedulerMappings
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void map_external_entity_to_job_entity()
        {
            //JarsEntityProcessor jjp = new JarsEntityProcessor();
            //IExternalEntityBase<int> jex = new ExternalJobHeader();
            //jex.Description = "";
            //jex.ExtRefId = 123.ToString();
            //jex.Location = "Full address";
            //jex.LineOfWork = "PLM";
            //jex.TargetDate = DateTime.Now.AddDays(1);
            //JarsJob jobBase = jjp.CreateJarsEntityFromExternalEntity(jex);


            //Assert.IsTrue(jobBase.ProgressStatus == "NEW");

        }


        [TestMethod]
        public void map_job_entity_to_appointment()
        {
            SchedulerControl sch = new SchedulerControl();
            sch.DataStorage.Appointments.CustomFieldMappings.Add(new AppointmentCustomFieldMapping("ENTITY","ENTITY"));

            
        }
    }
}
