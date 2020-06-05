using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//the code from this is an adaptation of an article at url using the AzureAdBearerAuth
//origin on SS https://forums.servicestack.net/t/how-to-replicate-windows-azure-active-directory-bearer-authentication/2697
// git hub https://github.com/cri-at-work/AzureADBearerAuthentication

namespace JARS.SS.Auth.Azure
{
    /// <summary>
    /// This class issues JWT tokens using azure, use this class when authenticating with azure to create tokens.
    /// </summary>
    public class AadJwtAuthProvider : JwtAuthProvider
    {
        internal IAppSettings _appSettings { get; private set; }
        internal string FederatedXmlUrl { get => _appSettings.Get<string>("aadjwt:FedKeyXmlUrl"); }
        internal List<RSAParameters> rsaParameterKeys { get => AzureFederatedMetadataRetriever.RetrieveRsaPublicKeys(FederatedXmlUrl); }
        internal IssuerSigningKeys signingKeys { get => AzureFederatedMetadataRetriever.GetSigningKeys(FederatedXmlUrl); }

        public AadJwtAuthProvider(IAppSettings appSettings) : base(appSettings)
        {
            _appSettings = appSettings;            
            FallbackPublicKeys = rsaParameterKeys;
            PublicKeyXml = rsaParameterKeys.First().ToPublicKeyXml();
            //HashAlgorithm = _appSettings.Get<string>("aadjwt:HashAlgorithm"); 
        }

        public override Task OnFailedAuthentication(IAuthSession session, IRequest httpReq, IResponse httpRes)
        {
            return base.OnFailedAuthentication(session, httpReq, httpRes);
        }
    }
}
