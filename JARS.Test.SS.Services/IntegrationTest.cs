using Funq;
using JARS.SS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using System;
using System.Linq;

namespace JARS.Test.SS.Services
{

    [TestClass]
    public class IntegrationTest
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(JarsJobService).Assembly) { }

            public override void Configure(Container container)
            { }
        }

        public IntegrationTest()
        {
            string licPath = "~/ServiceStackLicense.txt".MapAbsolutePath();
            Licensing.RegisterLicenseFromFileIfExists(licPath);
            appHost = new AppHost()
                .Init()
                .Start(BaseUri);
        }

        [TestCleanup]
        public void OneTimeTearDown() => appHost.Dispose();

        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [TestMethod]
        public void Can_call_Hello_Service()
        {
            var client = CreateClient();

            //var response = client.Get(new GetQLJob { Id = 1, FetchLazy = false });

            //Assert.IsTrue(response.Job != null);
        }
    }
}
