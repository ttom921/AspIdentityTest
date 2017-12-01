using AuthServer.SharedSettings.Settings;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(CoreApiSettings.ApiResource.Name, CoreApiSettings.ApiResource.DisplayName)
                {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName, JwtClaimTypes.Email }
                }
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // Core JavaScript Client
                new Client
                {
                    ClientId = CoreApiSettings.Client.ClientId,
                    ClientName = CoreApiSettings.Client.ClientName,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,////来允许返回Access Token
                    AccessTokenLifetime = 60 * 10,
                    AllowOfflineAccess = true,//我们还需要获取Refresh Token,这里离线是指用户和网站之间断开了

                    RedirectUris =           { CoreApiSettings.Client.RedirectUri, CoreApiSettings.Client.SilentRedirectUri },
                    PostLogoutRedirectUris = { CoreApiSettings.Client.PostLogoutRedirectUris },
                    AllowedCorsOrigins =     { CoreApiSettings.Client.AllowedCorsOrigins },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        CoreApiSettings.ApiResource.Name
                    }
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                //
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //加入email
                new IdentityResources.Email()
            };
        }
    }
}
