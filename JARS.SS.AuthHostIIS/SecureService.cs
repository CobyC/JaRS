using ServiceStack;

namespace JARS.SS.AuthHostIIS
{
    [Authenticate]
    public class SecureService : Service
    {
        public object Any(Secure request)
        {
            return new SecureResponse { Result = "If you see this you are authenticated" };
        }
    }

    [Route("/secure/")]
    public class Secure
    { }

    public class SecureResponse
    {
        public string Result { get; set; }
    }
}