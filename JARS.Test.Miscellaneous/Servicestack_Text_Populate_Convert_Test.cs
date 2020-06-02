using JARS.Data.FakeData;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Test.Miscellaneous
{
    [TestClass]
    public class Servicestack_Text_Populate_Convert_Test
    {
        class simpleJob
        {
            public DateTime? ActualStartDate { get; set; }
            public DateTime? ActualEndDate { get; set; }
            public string Priority { get; set; }

            public string SomeRandomValue { get; set; }
        }

        [TestMethod]
        public void Populate_With_Test()
        {
            simpleJob sj = new simpleJob();
            sj.ActualStartDate = DateTime.Now.AddDays(-0.8);
            sj.ActualEndDate = DateTime.Now.AddDays(-1);
            
            //see if only passes properties are changed
            JarsJob job = FakeDataHelper.FakeJarsJobs[0];
            job.LabelKey = "6";
            job.Priority = "10";
            

           job = job.PopulateWith(sj);

            Assert.IsTrue(job.ActualStartDate == sj.ActualStartDate);


        }

        [TestMethod]
        public void Convert_To_Test()
        {
            //see if all properties are changed
            simpleJob sj = new simpleJob();
            sj.ActualStartDate = DateTime.Now.AddDays(-0.5);
            sj.ActualEndDate = DateTime.Now.AddDays(-1);
            //see if only passes properties are changed
            JarsJob job = FakeDataHelper.FakeJarsJobs[0];
            job.LabelKey = "6";
            job.StatusKey = "6";

            sj = job.ConvertTo(sj);

            Assert.IsTrue(job.ActualStartDate == sj.ActualStartDate);
        }
    }
}
