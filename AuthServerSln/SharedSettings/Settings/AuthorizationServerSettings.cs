using System;
using System.Collections.Generic;
using System.Text;

namespace SharedSettings.Settings
{
    public class AuthorizationServerSettings
    {
#if DEBUG
        public static string AuthorizationServerBase = "http://localhost:5000";
#else
        public static (string Path, string Password) Certificate = (@"D:\Projects\test\socialnetwork.pfx", "Bx@steel");
        public static string AuthorizationServerBase = "https://localhost:5000";
#endif
    }
}
