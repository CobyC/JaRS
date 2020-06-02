using Funq;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using JARS.SS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;

namespace JARS.Test.SS.Services
{
    [TestClass]
    public class Jars_Integration_Tests
    {
        const string BaseUri = "http://localhost:2211/";
        private readonly ServiceStackHost appHost;

        /// <summary>
        /// This is the SelfHost class, it acts as the host of the services
        /// </summary>
        class AppHost : AppSelfHostBase
        {
            //for this test we will only use the basic classes that will ship with jars, and not the extension classes and services.
            public AppHost() : base(nameof(Jars_Integration_Tests), typeof(JarsJobService).Assembly)
            { }

            public override void Configure(Container container)
            { }
        }

        /// <summary>
        /// Start up the service host in the constructor.
        /// </summary>
        public Jars_Integration_Tests()
        {
            JarsCore.Container = MEFBusinessLoader.Init();
            //MEFLoader.Init();
            string licPath = "~/ServiceStackLicense.txt".MapAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licPath);
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [TestCleanup]
        public void OneTimeTearDown() => appHost.Dispose();

        /// <summary>
        /// This methods creates a client instance
        /// </summary>
        /// <returns>returns a service stack client instance.</returns>
        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [TestMethod]
        public void Create_JarsJobs_Service()
        {
            var client = CreateClient();

            JarsJob jj = new JarsJob { Description = $"Test Jars Job 0", Location = $"Test Location JarsJob 0", ExtRefId = "0", StatusKey = "0", LabelKey = "0", CreatedBy = $"TEST 0" };
            jj.JobLines.Add(new JarsJobLine { ShortDescription = $"Test JarsJobLine on Job 0", LineNum = 1, LineCode = "CODE0", OriginalQty = 0 });

            List<JarsJob> jjl = new List<JarsJob>();
            for (int i = 1; i <= 5; i++)
            {
                JarsJob jji = new JarsJob { Description = $"Test Jars Job {i}", Location = $"Test Location JarsJob {i}", ExtRefId = i.ToString(), StatusKey = $"{i}", LabelKey = $"{i}", CreatedBy = $"TEST {i}" };
                jji.JobLines.Add(new JarsJobLine { ShortDescription = $"Test JarsJobLine on Job {i}", LineNum = 1, LineCode = "CODE1", OriginalQty = i });
                jjl.Add(jji);
            }
            //store only takes Full jobs
            var rs = client.Post(new StoreJobs { Jobs = new List<JarsJobDto>(new[] { jj.ConvertTo<JarsJobDto>() }) });
            Assert.IsTrue(rs.Jobs[0].Id > 0);

            rs = client.Post(new StoreJobs { Jobs = jjl.ConvertAllTo<JarsJobDto>().ToList() });
            Assert.IsTrue(rs.Jobs.Count > 0);

            var rg = client.Get(new GetJarsJob { Id = 1 });
            Assert.IsTrue(rg.Jobs != null);

            rg = client.Get(new GetJarsJob { Id = 1 });
            Assert.IsTrue(rg.Jobs != null);
        }


        [TestMethod]
        public void Create_JarsJobs_Service_GetNested_Objects()
        {
            var client = CreateClient();

            //var rg = client.Get(new GetJarsUserRole { Id = 1 });
            //Assert.IsTrue(rg.RoleDto != null);

            //rg = client.Get(new GetJarsUserRole { Id = 1, FetchAsDto = false });
            //Assert.IsTrue(rg.Role != null);

            //var ua = client.Get(new GetJarsUserRole { Id = 1 });
            //Assert.IsTrue(ua.RoleDto != null);

            //ua = client.Get(new GetJarsUserRole { Id = 1, FetchAsDto = false });
            //Assert.IsTrue(ua.Role != null);
        }

    }
}
