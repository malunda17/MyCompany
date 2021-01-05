using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyCompany.IdentityService.Configuration
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ClaimServiceApi"),
                new ApiScope("LogServiceApi")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5006/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:5006/signout-callback-oidc" },
                    //FrontChannelLogoutUri = "https://localhost:5006/signout-oidc",
                    ClientSecrets =
                    {
                        new Secret("123456".Sha256())
                    },
                    AllowedScopes= new List<string> 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ClaimServiceApi"
                    },
                    RequireConsent = false,
                    AllowOfflineAccess = true
                }
            };
    }
}