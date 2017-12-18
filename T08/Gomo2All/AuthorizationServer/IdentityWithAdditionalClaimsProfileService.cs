using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer
{
    using AuthorizationServer.Models;
    using IdentityModel;
    using IdentityServer4;
    using IdentityServer4.Extensions;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;

    //取得client的role和claim
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        public  IdentityWithAdditionalClaimsProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
            //設定角色的Claim
            await SetRolesClaim(claims, user);
            //new Claim(JwtClaimTypes.Role, "admin"),
            //new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"),
            //new Claim(JwtClaimTypes.Role, "dataEventRecords.user"),
            //new Claim(JwtClaimTypes.Role, "dataEventRecords"),
            //new Claim(JwtClaimTypes.Role, "securedFiles.user"),
            //new Claim(JwtClaimTypes.Role, "securedFiles.admin"),
            //new Claim(JwtClaimTypes.Role, "securedFiles")
            //
           
            //
           
            //if (user.DataEventRecordsRole == "dataEventRecords.admin")
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.user"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords"));
            //    claims.Add(new Claim(JwtClaimTypes.Scope, "dataEventRecords"));
            //}
            //else
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.user"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords"));
            //    claims.Add(new Claim(JwtClaimTypes.Scope, "dataEventRecords"));
            //}

            //if (user.SecuredFilesRole == "securedFiles.admin")
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.admin"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.user"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles"));
            //    claims.Add(new Claim(JwtClaimTypes.Scope, "securedFiles"));
            //}
            //else
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles.user"));
            //    claims.Add(new Claim(JwtClaimTypes.Role, "securedFiles"));
            //    claims.Add(new Claim(JwtClaimTypes.Scope, "securedFiles"));
            //}

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            context.IssuedClaims = claims;
        }

       

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
       
        /// <summary>
        /// 設定角色的claims
        /// </summary>
        /// <param name="claims"></param>
        private async Task SetRolesClaim(List<Claim> claims, ApplicationUser user)
        {
            //最高管理者  Admin 1
            //代理商     Agent 2
            //經銷商     Dealer 3
            //車廠管理者 ShopManager 4
            //車廠技師 ShopTechnician 5
            //車廠員工 ShopStaff 6
            //會員 User 7
            //一般 General 8
            //測試人員1 Test1 16
            //測試人員2 Test2 17
            var Roles = await _userManager.GetRolesAsync(user);
            if (Roles.Contains("Admin") == true)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "General"));
            }
            //代理商     Agent 2
            if (Roles.Contains("Agent"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Agent"));
            }
            //經銷商     Dealer 3
            if (Roles.Contains("Dealer"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Dealer"));
            }
            //車廠管理者 ShopManager 4
            if (Roles.Contains("ShopManager"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "ShopManager"));
            }
            //車廠技師 ShopTechnician 5
            if (Roles.Contains("ShopTechnician"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "ShopTechnician"));
            }
            //車廠員工 ShopStaff 6
            if (Roles.Contains("ShopStaff"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "ShopStaff"));
            }
            //會員 User 7
            if (Roles.Contains("User"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "User"));
            }
            //一般 General 8
            if (Roles.Contains("General"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "General"));
            }
            if (Roles.Contains("Test1"))
            {
                claims.Add(new Claim(JwtClaimTypes.Role, "Test1"));
            }
            //測試人員1 Test1 16
            //測試人員2 Test2 17
        }
    }
}
