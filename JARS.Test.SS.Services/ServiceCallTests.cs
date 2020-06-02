using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.Entities;
using JARS.SS.DTOs;
using JARS.SS.DTOs.Utils;
using JARS.SS.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using ServiceStack.Testing;
using ServiceStack.Text;

namespace JARS.Test.SS.Services
{
    [TestClass]
    public class ServiceCallTests
    {
        private readonly ServiceStackHost appHost;

        public ServiceCallTests()
        {            
            JarsCore.Container = MEFBusinessLoader.Init();
            appHost = new BasicAppHost()
            {
                ConfigureAppHost = cfg => { JsConfig.MaxDepth = 2; }
            };
            appHost.Init();
            //appHost.Container.AddTransient<QLJobService>();
            appHost.Container.AddTransient<ResourceGroupService>();
            appHost.Container.AddTransient<ResourceService>();
        }

        [TestCleanup]
        public void OneTimeTearDown() => appHost.Dispose();

        [TestMethod]
        public void Can_call_QLJobServices()
        {
            //var service = appHost.Container.Resolve<QLJobService>();

            //var response = (GetQLJobResponse)service.Any(new GetQLJob { Id = 1 });
            //var findResponse = (QLJobsResponse)service.Any(new FindQLJobs { ResourceID = 1, FetchLazy = false });
            //Assert.IsTrue(findResponse.Jobs.Count > 0);

            //service.Any(new QLJobsCrudNotification { FromUserName = "test", Selector = "store." });


            //Assert.IsNotNull(response.Job);
        }

        [TestMethod]
        public void Return_Resource_WithGroup_Solve_Circular_Ref_Stack_Overflow()
        {
            //var rService = appHost.Container.Resolve<ResourceService>();
            //var findResponse = (ResourcesResponse)rService.Any(new FindResources() { IsActive = true, FetchEagerly = true });
            ////convert to client entities
            //List<JarsResource> clientResources = findResponse.Resources.ConvertAll(x=> x.ConvertTo<JarsResource>());
            //Assert.IsNotNull(clientResources);//this should now contain only client side entities after conversion

            //var rgService =  appHost.Container.Resolve<ResourceGroupService>();
            //var findResourceGroups = (ResourceGroupsResponse)rgService.Any(new FindResourceGroups() { IsActive = true, FetchEagerly = true});
            //List<JarsResourceGroup> grouptResources = findResourceGroups.Groups.ConvertAll(x => x.ConvertTo<JarsResourceGroup>());
            //Assert.IsNotNull(grouptResources);//this should now contain only client side entities after conversion

            //non eagerly
            var rService1 = appHost.Container.Resolve<ResourceService>();
            var findResponse1 = (ResourcesResponse)rService1.Any(new FindResources() { IsActive = true, FetchEagerly = false });
            var clientResources1 = findResponse1.Resources.ConvertAllTo<JarsResource>();
            Assert.IsNotNull(clientResources1);//this should now contain only client side entities after conversion

            var rgService1 = appHost.Container.Resolve<ResourceGroupService>();
            var findResourceGroups1 = (ResourceGroupsResponse)rgService1.Any(new FindResourceGroups() { IsActive = true, FetchEagerly = false });
            var grouptResources1 = findResourceGroups1.Groups.ConvertAllTo<JarsResourceGroup>();
            Assert.IsNotNull(grouptResources1);//this should now contain only client side entities after conversion


        }
    }
}

