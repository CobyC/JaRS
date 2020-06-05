using JARS.SS.DTOs;
using ServiceStack;

namespace JARS.SS.Services
{
    [Authenticate]
    public class ExternalJobService : ServicesBase
    {
        public object Any(FindExternalJobs request)
        {
            ExternalJobsResponse response = new ExternalJobsResponse();
            //response.ExternalJobs = FakeDataHelper.FakeExternalJobs.ToList();

            return response;
        }
    }
}
