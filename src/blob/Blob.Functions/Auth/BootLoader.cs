using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySundial.Reflections.Services.Functions
{
    /// <summary>
    /// GitHub/Docs: https://github.com/Azure-Samples/ms-identity-dotnet-webapi-azurefunctions
    /// App_ID: 653f20c0-d49b-48f3-a471-1ad0173fc6aa
    /// TENANT_ID: ad6529dd-8db1-4015-a53d-6ae395fc7e39
    /// https://login.microsoftonline.com/goodtocode.onmicrosoft.com/oauth2/v2.0/authorize?response_type=code&client_id=653f20c0-d49b-48f3-a471-1ad0173fc6aa&redirect_uri=http://localhost:7071/&scope=openid
    /// Get Code: https://login.microsoftonline.com/[TENANT_NAME].onmicrosoft.com/oauth2/v2.0/authorize?response_type=code&client_id=[APPLICATION_ID]&redirect_uri=http://localhost:7071/&scope=openid
    /// Get Access Token:
    ///     curl -X POST \
    ///     https://login.microsoftonline.com/[TENANT_NAME].onmicrosoft.com/oauth2/v2.0/token \
    ///     -H 'Accept: */*' \
    ///     -H 'Cache-Control: no-cache' \
    ///     -H 'Connection: keep-alive' \
    ///     -H 'Content-Type: application/x-www-form-urlencoded' \
    ///     -H 'Host: login.microsoftonline.com' \
    ///     -H 'accept-encoding: gzip, deflate' \
    ///     -H 'cache-control: no-cache' \
    ///     -d 'redirect_uri=http%3A%2F%2Flocalhost:7071&client_id=[APPLICATION_ID]&grant_type=authorization_code&code=[ACCESS_CODE]&client_secret=p@ssword1&scope=https%3A%2F%funcapi.[TENANT_NAME].onmicrosoft.com%2F/user_impersonation'
    /// Test: http://localhost:7071/Authenticated (with Access Token as Authorization Bearer header)
    ///     curl -X GET -H "Authorization: Bearer [ACCESS TOKEN]" -H "Content-Type: application/json" http://localhost:7071/Authenticated
    ///     curl -X GET -H "Authorization: Bearer [ACCESS TOKEN]" -H "Content-Type: application/json" https://management.azure.com/subscriptions/[SUBSCRIPTION_ID]/providers/Microsoft.Web/sites?api-version=2016-08-01
    /// </summary>
    public static class BootLoader
    {
        [FunctionName("BootLoader")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",
            Route = "{requestedRoute}")] HttpRequest req,
            string requestedRoute,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            requestedRoute = requestedRoute.ToLower();
            switch (requestedRoute)
            {
                case "anonymous":
                    return Anonymous(req, log);
                case "authenticated":
                    return await Authenticated(req, log);
                default:
                    break;
            }
            return (ActionResult)new OkObjectResult(requestedRoute);
        }

        private static IActionResult Anonymous(HttpRequest req, ILogger log)
        {
            return (ActionResult)new OkObjectResult("anonymous");
        }

        private static async Task<IActionResult> Authenticated(HttpRequest req, ILogger log)
        {
            var accessToken = GetAccessToken(req);
            ClaimsPrincipal claimsPrincipal = await ValidateAccessToken(accessToken, log);
            if (claimsPrincipal != null)
            {
                return (ActionResult)new OkObjectResult(claimsPrincipal.Identity.Name);
            }
            else
            {
                return (ActionResult)new UnauthorizedResult();
            }
        }

        private static string GetAccessToken(HttpRequest req)
        {
            var authorizationHeader = req.Headers?["Authorization"];
            string[] parts = authorizationHeader?.ToString().Split(null) ?? new string[0];
            if (parts.Length == 2 && parts[0].Equals("Bearer"))
                return parts[1];
            return null;
        }

        private static async Task<ClaimsPrincipal> ValidateAccessToken(string accessToken, ILogger log)
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

            try
            {
                SecurityToken securityToken;
                var claimsPrincipal = tokenValidator.ValidateToken(accessToken, validationParameters, out securityToken);
                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
            return null;
        }
    }
}

