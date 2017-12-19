using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Extensions
{
    public static class UserManagerExtensions
    {
        public static Task<ApplicationUser> FindByNameAsync(this UserManager<ApplicationUser> um, string name)
        {
            var finduser= um?.Users?.SingleOrDefault(x => (x.NormalizedUserName == name.ToUpper()) && (x.DelFlag == 0));
            return Task.FromResult<ApplicationUser>(finduser);
        }
        public static Task<ApplicationUser> FindByEmailAsync(this UserManager<ApplicationUser> um, string email)
        {
            var finduser = um?.Users?.SingleOrDefault(x => (x.NormalizedEmail == email.ToUpper()) && (x.DelFlag == 0));
            return Task.FromResult<ApplicationUser>(finduser);
        }
        
    }
}
