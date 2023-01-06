using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace HitchFix_Identity
{
    public static class SD
    {
        public const string Admin = "admin";
        public const string Customer = "customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope>
            {
                new ApiScope("hitchscope", "Hitch Server"),
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };
        

        public static IEnumerable<Client> Clients =>
             new List<Client>
            {
                new Client
                {
                    ClientId = "service.client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1", "api2.read_only" }
                },
                // web client (FrontEnd)
                new Client
                {
                    ClientId = "hitch",
                    ClientSecrets = { new Secret("secret".Sha256()) }, // can put secret in appsettings.json
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "hitchscope", IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.Profile, JwtClaimTypes.Role },
                    RedirectUris={ "https://localhost:7255/signin-oidc" },
                    PostLogoutRedirectUris={ "https://localhost:7255/signout-callback-oidc" },
                }
            };
    }
}
