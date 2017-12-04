using System;
using System.Collections.Generic;
using System.Text;

namespace SalesApi.Shared.Settings
{
    public class SalesApiSettings
    {
#if DEBUG
        public const string UriBase = "http://localhost:4200";
#else
        public const string UriBase = "http://localhost:4200";
#endif
        public static string CorsPolicyName = "sales";
        public static string CorsOrigin = UriBase;
        public static (string Name, string DisplayName) ApiResource = ("salesapi", "销售系统 APIs");
        public static (string ClientId, string ClientName, string RedirectUri, string SilentRedirectUri, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("sales", "销售系统", $"{UriBase}/login-callback", $"{UriBase}/silent-renew.html", $"{UriBase}/index.html", $"{UriBase}");
    }
}
