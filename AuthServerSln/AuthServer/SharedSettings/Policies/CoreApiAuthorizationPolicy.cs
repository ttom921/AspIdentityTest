using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.SharedSettings.Policies
{
    public class CoreApiAuthorizationPolicy
    {
        public const string PolicyName = "Admin";
        public const string ClaimName = "管理者";
        public const string ClaimValue = "admin";
    }
}
