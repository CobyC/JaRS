using System;
using System.Collections.Generic;
using JARS.Core.Entities;
using JARS.Data.FakeData;
using JARS.Entities;
using JARS.SS.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace JARS.Test.EntityConversionsAndMappings
{




    [TestClass]
    public class EntityConversionTests
    {
        [TestMethod]
        public void Convert_From_Entity_to_DTO_to_Client_using_SS()
        {
            //get business entity
            var jJob = FakeDataHelper.FakeJarsJobs[0];

            var jDto = jJob.ConvertTo<JarsJobDto>();

            var jobVM = jDto.ConvertTo<JarsJob>();

            //change a value on jobViewModel and persist back
            jobVM.Description = "I HAVE CHANGED!!";

            jDto = jobVM.ConvertTo<JarsJobDto>();

            jJob = jDto.ConvertTo<JarsJob>();

        }

        [TestMethod]
        public void Convert_Partial_Object_to_Update_Existing_or_create_new()
        {
            //take a object with a few properties and update an existing object using only the properties from the mini version
            //create a new one
            var miniJob = new MiniJob();
            miniJob.Description = "This is a property change on a new mini job";
            miniJob.StatusKey = 1234;

            var jobDto = miniJob.ConvertTo<JarsJobDto>();
            var jJob = jobDto.ConvertTo<JarsJob>();
            Assert.AreEqual(miniJob.Description, jJob.Description);

            //update from an existing one.
            jJob = FakeDataHelper.FakeJarsJobs[2];
            jobDto = jJob.ConvertTo<JarsJobDto>();
            miniJob = miniJob.PopulateWith(jobDto);
            Assert.AreEqual(jJob.Id, miniJob.Id);

            //add an additional line to an existing job
            miniJob.JobLines.Add(new MiniLine() { LineCode = "NEWMINI", LineNum = 3, ResourceId = jJob.ResourceId });
            jobDto = miniJob.ConvertTo<JarsJobDto>();
            jJob = jobDto.ConvertTo<JarsJob>();
            Assert.IsTrue(jJob.JobLines.Count == miniJob.JobLines.Count);
        }
    }


    #region Test Classes
    class MiniJob : EntityBase<int>
    {
        public string Description { get; set; }
        public int StatusKey { get; set; }
        public IList<MiniLine> JobLines { get; set; }
        public int ResourceId { get; set; }

    }

    class MiniLine : EntityBase<int>
    {
        public int LineNum { get; set; }
        public string LineCode { get; set; }
        public int ResourceId { get; set; }
    }

    class MiniResource : EntityBase<int>
    {
        public string DisplayName { get; set; }

        public bool IsActive { get; set; }
    }
    #endregion
}
