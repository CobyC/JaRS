///this implementation was taken from the article and solution at
///https://forums.servicestack.net/t/how-to-replicate-windows-azure-active-directory-bearer-authentication/2697/6
///with git hub
///https://github.com/cri-at-work/AzureADBearerAuthentication
///
using ServiceStack;
using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JARS.SS.Auth.Azure
{
    public class AzureFederatedMetadataRetriever
    {
        internal static List<RSAParameters> RetrieveRsaPublicKeys(string AzureFedUrl)
        {
            var publicKeys = new List<RSAParameters>();

            IssuerSigningKeys issuerSigningKeys = GetSigningKeys(AzureFedUrl);
            foreach (var token in issuerSigningKeys.Tokens)
            {
                var provider = (RSACryptoServiceProvider)token.Certificate.PublicKey.Key;
                publicKeys.Add(provider.ExportParameters(false));
            }

            if (publicKeys.Count == 0)
            {
                throw new TokenException("No public key found at " + AzureFedUrl);
            }
            return publicKeys;
        }

        internal static IssuerSigningKeys GetSigningKeys(string AzureFedUrl)
        {
            string issuer = "";
            List<X509SecurityToken> tokens = new List<X509SecurityToken>();

            var responseStatus = AzureFedUrl.GetResponseStatus();

            if (responseStatus.HasValue && responseStatus.Value == System.Net.HttpStatusCode.OK)
            {
                using (XmlReader xmlReader = XmlReader.Create(AzureFedUrl))
                {
                    var mdSerializer = new MetadataSerializer { CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None };
                    MetadataBase metadata = mdSerializer.ReadMetadata(xmlReader);
                    var entDescriptor = (EntityDescriptor)metadata;

                    //get the issues
                    if (!string.IsNullOrWhiteSpace(entDescriptor.EntityId.Id))
                        issuer = entDescriptor.EntityId.Id;

                    //get security token info
                    SecurityTokenServiceDescriptor stsd = entDescriptor.RoleDescriptors.OfType<SecurityTokenServiceDescriptor>().First();
                    if (stsd == null)
                        throw new InvalidOperationException("No SecurityTokenServiceType descriptor in metadata.");

                    IEnumerable<X509RawDataKeyIdentifierClause> x509DataClauses =
                               stsd.Keys.Where(key => key.KeyInfo != null && (key.Use == KeyType.Signing || key.Use == KeyType.Unspecified))
                               .Select(key => key.KeyInfo.OfType<X509RawDataKeyIdentifierClause>().First());

                    tokens.AddRange(x509DataClauses.Select(token => new X509SecurityToken(new X509Certificate2(token.GetX509RawData()))));
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to connect to federated trust url.");
            }
            return new IssuerSigningKeys() { Issuer = issuer, Tokens = tokens };
        }
    }
}
