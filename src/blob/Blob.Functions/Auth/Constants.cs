using System.Globalization;
using System.Collections.Generic;

namespace MySundial.Reflections.Services.Functions
{
    // demo code, usually want to pull these from key vault or config etc.
    internal static class Constants
    {
        internal static string audience = "https://mysundial-mobile.goodtocode.onmicrosoft.com/user_impersonation"; // Get this value from the expose an api, audience uri section example https://appname.tenantname.onmicrosoft.com
        internal static string clientID = "653f20c0-d49b-48f3-a471-1ad0173fc6aa"; // this is the client id, also known as AppID. This is not the ObjectID
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
}