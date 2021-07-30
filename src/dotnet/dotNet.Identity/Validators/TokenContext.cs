using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Identity
{
    public class TokenContext
    {
        private readonly string _audience;
        private readonly string _clientId;
        private readonly string _tenantName;
        private readonly IList<string> _validIssuers;

        public string Authority { get { return _tenantName == null ? "https://login.microsoftonline.com/common/" : $"https://login.microsoftonline.com/{_tenantName}/v2.0"; } }
        public string WellKnownUrl { get { return $"{Authority}/.well-known/openid-configuration"; } }

        private TokenContext(string audience, string clientId, string tenantId)
        {
            _audience = audience;
            _clientId = clientId;
            _validIssuers ??= new List<string>()
            {
                $"https://login.microsoftonline.com/{tenantId}/",
                $"https://login.microsoftonline.com/{tenantId}/v2.0",
                $"https://login.windows.net/{tenantId}/",
                $"https://login.microsoft.com/{tenantId}/",
                $"https://sts.windows.net/{tenantId}/"
            };
        }

        private TokenContext(string audience, string clientId, string tenantId, string tenantName) : this(audience, clientId, tenantId)
        {
            _tenantName = tenantName;
        }

        public TokenContext(string audience, string clientId, string tenantId, IList<string> validIssuers) : this(audience, clientId, tenantId)
        {
            _validIssuers = validIssuers;
        }

        public async Task<bool> IsAuthenticatedAsync(HttpRequest item)
        {
            var accessToken = item.Bearer();
            ClaimsPrincipal claimsPrincipal = await ValidateAccessTokenAsync(accessToken);
            return claimsPrincipal != null;
        }

        private async Task<ClaimsPrincipal> ValidateAccessTokenAsync(string accessToken)
        {
#if DEBUG
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
#endif

            ConfigurationManager<OpenIdConnectConfiguration> configManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(
                    WellKnownUrl,
                    new OpenIdConnectConfigurationRetriever());

            var config = await configManager.GetConfigurationAsync();
            ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudiences = new List<string> { _audience, _clientId },
                ValidIssuers = _validIssuers,
                IssuerSigningKeys = config.SigningKeys
            };

            SecurityToken securityToken;
            var claimsPrincipal = tokenValidator.ValidateToken(accessToken, validationParameters, out securityToken);
            return claimsPrincipal;
        }
    }
}