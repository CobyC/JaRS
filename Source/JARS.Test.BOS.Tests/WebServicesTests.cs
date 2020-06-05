using JARS.BOS.SS.DTOs;
using JARS.BOS.SS.Services;
using JARS.Business.Bootstrap;
using JARS.Core;
using JARS.SS.DTOs.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;
using ServiceStack.Testing;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARS.Test.BOS.Tests
{
    [TestClass]
    public class WebServicesTests
    {
        private readonly ServiceStackHost appHost;

        public WebServicesTests()
        {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            JarsCore.Container = MEFBusinessLoader.Init();
            appHost = new BasicAppHost()
            {
                ConfigureAppHost = cfg => { JsConfig.MaxDepth = 2; }
            };
            appHost.Init();

            appHost.Container.AddTransient<BOSEntityService>();

        }

        [TestCleanup]
        public void OneTimeTearDown() => appHost.Dispose();

        [TestMethod]
        public void Can_call_BOS_Services()
        {
            var service = appHost.Container.Resolve<BOSEntityService>();

            var response = (BOSEntityResponse)service.Any(new GetBOSEntity { Id = 5 });
            Assert.IsNotNull(response.BOSEntity);

            var findResponse = (BOSEntitiesResponse)service.Any(new FindBOSEntities { ResourceId = "1" });
            Assert.IsTrue(findResponse.BOSEntities.Count > 0);

            findResponse = service.Any(new FindBOSEntities());
            Assert.IsTrue(findResponse.BOSEntities.Count > 0);

            var notify = new BOSEntitiesNotification { FromUserName = "test", Selector = SelectorTypes.store };
            service.Any(notify);

        }
    }
}
