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
                    ClientName = "Angular-Client",
                    ClientId = "angular-client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://mycompany.clientapplication:4200/signin-callback",
                        "http://mycompany.clientapplication:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "companyApi"
                    },
                    AllowedCorsOrigins = { "http://mycompany.clientapplication:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://mycompany.clientapplication:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
 
                }
                ,
                new Client
                {
                    ClientId = "mvc",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://mycompany.agentapplication:5006/signin-oidc"},
                    PostLogoutRedirectUris = { "https://mycompany.agentapplication:5006/signout-callback-oidc" },
                    //FrontChannelLogoutUri = "https://mycompany.agentapplication:5006/signout-oidc",
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