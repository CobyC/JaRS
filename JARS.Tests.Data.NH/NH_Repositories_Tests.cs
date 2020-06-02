using JARS.BOS.Data;
using JARS.BOS.Entities;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Core.Data.Interfaces.Repositories;
using JARS.Core.Interfaces.Repositories;
using JARS.Data.NH.Jars;
using JARS.Data.NH.Jars.Interfaces;
using JARS.Data.NH.Jars.Repositories;
using JARS.Data.NH.Repositories;
using JARS.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace JARS.Tests.Data.NH
{
    /// <summary>
    /// This class is used for the testing of the repository classes, this does not implement MEF and DI
    /// </summary>
    [TestClass]
    public class NH_Repositories_Tests
    {
        //NHDataContext _DBContext;
        [Import]
        IDataRepositoryFactory _repFactory; //<-- this calls the repository factory that also creates the connection to the database


        public NH_Repositories_Tests(IDataRepositoryFactory repFactory)
        {
            _repFactory = repFactory;
        }

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void Initialize()
        {
            JarsCore.Container = MEFBusinessLoader.Init(); //<-- this will always be the first call made by application startup, should only be called once.
            JarsCore.Container.SatisfyImportsOnce(this); //<-- this basically creates the instances of every item that is marked with [Import]            
        }


        [TestMethod]
        void use_repository_factory_to_get_non_generic_repositories_and_create_record()
        {
            IJarsJobRepository jarsRep = _repFactory.GetDataRepository<IJarsJobRepository>();
           

            Assert.IsNotNull(jarsRep);
            //Assert.IsNotNull(qlRep);

            //test create
            JarsJob jj = jarsRep.CreateUpdate(new JarsJob(), "NONGEN_FACT_TEST");
            Assert.IsTrue(jj.Id > 0);

           
            //test read
            jj = jarsRep.GetById((long)1);
            Assert.IsTrue(jj.Id == 0);

           
        }
        /// <summary>
        /// Where we test to see if we can get an existing proxy class using the factory class
        /// </summary>
        [TestMethod]
        public void obtain_generic_base_repository_using_MEF_and_read_and_create_records()
        {
            //now only using mef test the obtainment
            IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars> jarsRep = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();
            //assert creation
            Assert.IsNotNull(jarsRep);
        }

        /// <summary>
        /// Where we test to see if we can get an existing proxy class using the factory class
        /// </summary>
        [TestMethod]
        public void obtain_generic_repository_using_MEF_and_read_and_create_records()
        {
            //now only using mef test the obtainment
            IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars> jarsRep = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<JarsJob, IDataContextNhJars>>();
            IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS> extRep = _repFactory.GetDataRepository<IGenericEntityRepositoryBase<BOSEntity, IDataContextBOS>>();
           
            //assert creation
            //Assert.IsNotNull(qlRep);
            Assert.IsNotNull(jarsRep);
            Assert.IsNotNull(extRep);

            //list the jobs from each repository
            IList<JarsJob> jarsJobs = jarsRep.GetAll();
            Assert.IsTrue(jarsJobs != null);

            IList<BOSEntity> extHeads = extRep.GetAll();
            Assert.IsTrue(extHeads != null);

           
            //greate a record
            JarsJob jj = jarsRep.CreateUpdate(new JarsJob(), "MEF_GEN_TEST");
            Assert.IsTrue(jj.Id > 0);

            BOSEntity xj = extRep.CreateUpdate(new BOSEntity(), "MEF_GEN_TEST");
            Assert.IsTrue(xj.Id > 0);

           
            //get a record from each repository
            JarsJob job = jarsRep.GetById((long)1);
            Assert.IsTrue(job.Id == 1);

            BOSEntity header = extRep.GetById(1);
            Assert.IsTrue(header.Id == 1);

           
        }

        [TestMethod]
        public void use_congrete_repository_classes_no_MEF_and_read_create_records()
        {
            JarsJobRepository jarsRepo = new JarsJobRepository(new DataContextNhJars());
            Assert.IsNotNull(jarsRepo);
           
            IList<JarsJob> jarsJobs = jarsRepo.GetAll();
            Assert.IsTrue(jarsJobs != null);

           
            //greate a record
            JarsJob jj = jarsRepo.CreateUpdate(new JarsJob(), "CONC_REP_TEST");
            Assert.IsTrue(jj.Id > 0);

           
            //get a record from each repository
            JarsJob job = jarsRepo.GetById((long)1);
            Assert.IsTrue(job.Id == 1);

           
        }

        [TestMethod]
        public void use_congrete_generic_repository_classes_no_MEF_and_read_create_records()
        {
            GenericEntityRepository<JarsJob, IDataContextNhJars> jarsRepo = new GenericEntityRepository<JarsJob, IDataContextNhJars>(new DataContextNhJars());
            //GenericEntityRepository<ExternalJobHeader, IDataContextNhExternal> extRepo = new GenericEntityRepository<ExternalJobHeader, IDataContextNhExternal>(new DataContextExternalNh());

            Assert.IsNotNull(jarsRepo);
           // Assert.IsNotNull(extRepo);

            IList<JarsJob> jarsJobs = jarsRepo.GetAll();
            Assert.IsTrue(jarsJobs != null);

            //IList<ExternalJobHeader> extHeads = extRepo.GetAll();
           // Assert.IsTrue(extHeads != null);

           
            //greate a record
            JarsJob jj = jarsRepo.CreateUpdate(new JarsJob(), "CONC_GENREP_TEST");
            Assert.IsTrue(jj.Id > 0);

           // ExternalJobHeader xj = extRepo.CreateUpdate(new ExternalJobHeader(), "CONC_GENREP_TEST");
           // Assert.IsTrue(xj.Id > 0);

           
            //get a record from each repository
            JarsJob job = jarsRepo.GetById((long)1);
            Assert.IsTrue(job.Id == 1);

           // ExternalJobHeader header = extRepo.GetById(1);
           // Assert.IsTrue(header.Id == 1);

           
        }


        [TestMethod]
        public void Create_Jobs_Base_and_QL()
        {
            //IJobRepository rep = _repFactory.GetDataRepository<IJobRepository>();
            IJarsJobRepository nhRep = _repFactory.GetDataRepository<IJarsJobRepository>();

            for (int i = 0; i < 100; i++)
            {

                JarsJob baseJob = new JarsJob
                {
                    StartDate = DateTime.Now.Subtract(new TimeSpan(2, 0, 0)),
                    EndDate = DateTime.Now,
                    Description = $"IJobBase Job {i}",
                    Location = $" IJobBase from QL job {i}",
                    //AdditionalJobProperty = $"This {i}"

                };

                JarsJob jobQl = new JarsJob
                {
                    StartDate = DateTime.Now.Subtract(new TimeSpan(2, 0, 0)),
                    EndDate = DateTime.Now,
                    Description = $"QL Job {i}",
                    Location = $"Test QL  {i}"
                };

                nhRep.CreateUpdate(baseJob, "DataCRUDTest");
                Assert.AreNotEqual(baseJob.Id, 0);

                nhRep.CreateUpdate(jobQl, "DataCRUDTest");
                Assert.AreNotEqual(jobQl.Id, 0);
            }

        }

        [TestMethod]
        public void List_Jobs()
        {
            IJarsJobRepository nhRep = _repFactory.GetDataRepository<IJarsJobRepository>();

            var jl = nhRep.GetAll();
            Assert.IsTrue(jl.Count > 0);

            var jobs = nhRep.GetAll();
            Assert.IsTrue(jobs.Count > 0);

        }

        [TestMethod]
        public void Create_Resource_Jobs_Attachment_and_Lines()
        {
            IJarsJobRepository nhRep = _repFactory.GetDataRepository<IJarsJobRepository>();
           
            //create op
            for (int i = 0; i < 5; i++)
            {
                JarsResource res = new JarsResource
                {
                    DisplayName = $"Test{i}",
                    ExtRef1 = $"T00{i}",
                    IsActive = true,
                    Skills = new List<JarsResourceSkill> { new JarsResourceSkill { MaxLevel = 10, Description = "Test Skill", DocumentCode = $"SK0{i}T" } },
                    MobileNo = $"0{i}234{i}2312{i}"
                };

                //res = resrep.CreateUpdate(res, "TEST");

                JarsJobAttachment ja = new JarsJobAttachment { Name = "Attach Test" };

                JarsJob baseJob = new JarsJob
                {
                    StartDate = DateTime.Now.Subtract(new TimeSpan(2, 0, 0)),
                    EndDate = DateTime.Now,
                    Description = $"IJob Job{i}",
                    Location = $" IJob for Testing job {i}",
                    ExtRefId = $"0{i}{i}{i}",
                    ResourceId = res.Id
                };
                baseJob.Attachments.Add(ja);

                JarsJobLine line = new JarsJobLine { Resource = res, LineCode = $"TEST0{i}", OriginalQty = 1, LineNum = 1, ExternalJobRef = baseJob.ExtRefId };

                baseJob.JobLines.Add(line);

                nhRep.CreateUpdate(baseJob, "TEST_LOOP");
            }
        }
       
    }
}
