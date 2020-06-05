using Funq;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System.Collections.Generic;

namespace JARS.Test.SS.Services
{
    [TestClass]
    public class ServerEvents_And_JsonClient_Tests
    {
        const string BaseUri = "http://localhost:2211/";
        private readonly ServiceStackHost appHost;

        /// <summary>
        /// This is the SelfHost class, it acts as the host of the services
        /// </summary>
        class AppHost : AppSelfHostBase
        {
            //for this test we will only use the basic classes that will ship with jars, and not the extension classes and services.
            public AppHost() : base(nameof(ServerEvents_And_JsonClient_Tests), typeof(JarsJobService).Assembly)//, typeof(QLJobHeaderService).Assembly)
            { }

            public override void Configure(Container container)
            {
                //because we use service events, we need to register the plugin for it.
                Plugins.Add(new ServerEventsFeature());
            }
        }

        /// <summary>
        /// Start up the service host in the constructor.
        /// </summary>
        public ServerEvents_And_JsonClient_Tests()
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
        public void OneTimeTearDown()
        {
            appHost.Dispose();
            JarsCore.Container.Dispose();
        }

        /// <summary>
        /// This methods creates an event client instance
        /// </summary>
        /// <returns>returns an event service stack client instance.</returns>
        public ServerEventsClient CreateEventClient() => new ServerEventsClient(BaseUri, channels: "test");

        /// <summary>
        /// This methods creates a client instance (jSonServiceClient)
        /// </summary>
        /// <returns>returns a service stack client instance.</returns>
        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [TestMethod]
        public void QL_get_jobheaders_information_standard_service_test()
        {
            var client = CreateClient();

            //IList<QLJobHeader> headerList = new List<QLJobHeader>();

            ////get single record
            //GetQLJobHeaderResponse hRes = client.Post(new GetQLJobHeader { Id = 101 });
            //Assert.IsTrue(hRes.JobHeader != null);

            ////get multiple records
            //QLJobHeadersResponse hdrsResp = client.Post(new FindQLJobHeaders());//should get all
            //Assert.IsNull(hdrsResp.JobHeader);
            //Assert.IsTrue(hdrsResp.JobHeaders.Count > 0);

            ////filter request
            //hdrsResp = client.Post(new FindQLJobHeaders { LineOfWorkCode = "DR" });
            //Assert.IsNull(hdrsResp.JobHeader);
            //Assert.IsTrue(hdrsResp.JobHeaders.Count > 0);
            //Assert.IsTrue(hdrsResp.JobHeaders.FindAll(j => j.LineOfWork != "DR").Count == 0);

            //hdrsResp = client.Post(new FindQLJobHeaders { Location = "15_" });
            //Assert.IsNull(hdrsResp.JobHeader);
            //Assert.IsTrue(hdrsResp.JobHeaders.Count > 0);
            //Assert.IsTrue(hdrsResp.JobHeaders.FindAll(j => j.Location.Contains("6_")).Count == 0);

        }

        [TestMethod]
        public void Create_JarsJobs_Service()
        {
            var client = CreateEventClient().Start();

            List<JarsJobBase> jl = new List<JarsJobBase>();
            for (int i = 0; i < 5; i++)
            {
                jl.Add(new JarsJob { Description = $"Test Job {i}", Location = $"Test Location {i}", ExtRefId = i.ToString(), StatusKey = $"{i}", LabelKey = $"{i}", CreatedBy = $"TEST {i}" });
            }

            //JobsCrudNotification jcn = new JobsCrudNotification { Selector = SelectorTypes.store.ToString(), Channel = "test", From = "user1", NotifyChanel = false };
            //jcn.Job = jl[0];
            //var r1 = client.ServiceClient.Post(jcn);
            //Assert.IsTrue(r1.Job.ID > 0);

            //JobsCrudNotification jcn1 = new JobsCrudNotification { Selector = SelectorTypes.store.ToString(), Channel = "test", From = "user1", NotifyChanel = false };
            //jcn1.Jobs = jl;
            //var r2 = client.ServiceClient.Post(jcn1);
            //Assert.IsTrue(r2.Jobs[0].ID > 0);

            //var r3 = client.ServiceClient.Post(new StoreJobs { Jobs = jl });
            //Assert.IsTrue(r1.Job.ID > 0);

            //var response = client.ServiceClient.Get(new GetJob { Id = 5, FetchLazy = false });
            var response = client.ServiceClient.Get(new GetJarsJob { Id = 5 });
            Assert.IsTrue(response.Jobs != null);
        }

    }
}
