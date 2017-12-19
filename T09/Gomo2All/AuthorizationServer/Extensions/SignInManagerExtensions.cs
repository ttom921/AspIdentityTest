using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Extensions
{
    public static class SignInManagerExtensions
    {
        public static async Task<SignInResult> PasswordSignInAsync(this SignInManager<ApplicationUser> sm,string userEmail, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManagerExtensions.FindByEmailAsync(sm.UserManager, userEmail);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            return await sm.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            
        }
    }
}
