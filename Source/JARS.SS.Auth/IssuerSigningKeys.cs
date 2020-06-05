using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace JARS.SS.Auth
{
    internal class IssuerSigningKeys
    {
        /// <summary>
        /// The token issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Signing tokens.
        /// </summary>
        public IEnumerable<X509SecurityToken> Tokens { get; set; }
    }
}
