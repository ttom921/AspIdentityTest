using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSettings.Settings
{
    public class CoreApiSettings
    {
        public const string UriBase = "http://localhost:5200";
        public static string CorsPolicyName = "default";
        public static string CorsOrigin = UriBase;
        public static (string Name, string DisplayName) ApiResource = ("coreapi", "核心系统 API");
        public static (string ClientId, string ClientName, string RedirectUri, string SilentRedirectUri, string PostLogoutRedirectUris, string AllowedCorsOrigins) Client =
            ("core", "核心系统", $"{UriBase}/login-callback", $"{UriBase}/silent-renew.html", $"{UriBase}/index.html", $"{UriBase}");
    }
}
