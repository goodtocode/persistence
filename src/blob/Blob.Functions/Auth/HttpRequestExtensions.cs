using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Blob.Functions
{
    public static class HttpRequestExtensions
    {
        private static class Constants
        {
            internal static string audience = "https://gtc-shared-blob.goodtocode.onmicrosoft.com/user_impersonation"; // Get this value from the expose an api, audience uri section example https://appname.tenantname.onmicrosoft.com
            //orig aud set - api://8c126e05-50d0-4638-8090-12b9a9f7fdfa
            internal static string clientID = "8c126e05-50d0-4638-8090-12b9a9f7fdfa"; // this is the client id, also known as AppID. This is not the ObjectID
            internal static string tenant = "goodtocode.com"; // "goodtocode.onmicrosoft.com"; // this is your tenant name
            internal static string tenantid = "ad6529dd-8db1-4015-a53d-6ae395fc7e39"; // this is your tenant id (GUID)

            // rest of the values below can be left as is in most circumstances
            internal static string aadInstance = "https://login.microsoftonline.com/{0}/v2.0";
            internal static string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);
            internal static List<string> validIssuers = new List<string>()
            {
                $"https://login.microsoftonline.com/{tenant}/",
                $"https://login.microsoftonline.com/{tenant}/v2.0",
                $"https://login.windows.net/{tenant}/",
                $"https://login.microsoft.com/{tenant}/",
                $"https://sts.windows.net/{tenantid}/"
            };
        }

        private static async Task<bool> IsAuthenticated(this HttpRequest item)
        {
            var accessToken = GetAccessToken(item);
            ClaimsPrincipal claimsPrincipal = await ValidateAccessToken(accessToken);
            if (claimsPrincipal != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string GetAccessToken(this HttpRequest item)
        {
            var authorizationHeader = item.Headers?["Authorization"];
            string[] parts = authorizationHeader?.ToString().Split(null) ?? new string[0];
            if (parts.Length == 2 && parts[0].Equals("Bearer"))
                return parts[1];
            return null;
        }

        private static async Task<ClaimsPrincipal> ValidateAccessToken(string accessToken)
        {
            var audience = Constants.audience;
            var clientID = Constants.clientID;
            var tenant = Constants.tenant;
            var tenantid = Constants.tenantid;
            var aadInstance = Constants.aadInstance;
            var authority = Constants.authority;
            var validIssuers = Constants.validIssuers;

            // Debugging purposes only, set this to false for production
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            ConfigurationManager<OpenIdConnectConfiguration> configManager =
                new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{authority}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever());

            var config = await configManager.GetConfigurationAsync();

            ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();

            // Initialize the token validation parameters
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                // App Id URI and AppId of this service application are both valid audiences.
                ValidAudiences = new[] { audience, clientID },

                // Support Azure AD V1 and V2 endpoints.
                ValidIssuers = validIssuers,
                IssuerSigningKeys = config.SigningKeys
            };

            SecurityToken securityToken;
            var claimsPrincipal = tokenValidator.ValidateToken(accessToken, validationParameters, out securityToken);
            return claimsPrincipal;
        }
    }
}

