using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name = "rc.scope",
                    UserClaims =
                    {
                        "rc.garndma"
                    }
                }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                 
                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:44354/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44354/Home/Index" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "rc.scope",
                    },

                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
               
            };
    }
}
