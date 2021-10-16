using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer.AuthServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resource_api1") { Scopes = { "api1.read", "api1.write", "api1.update", "api1.delete" }},
                new ApiResource("resource_api2") { Scopes = { "api2.read", "api2.write", "api2.update", "api2.delete" }}
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api1.read", "Read permission for API 1"),
                new ApiScope("api1.write", "Write permission for API 1"),
                new ApiScope("api1.update", "Update permission for API 1"),
                new ApiScope("api1.delete", "Delete permission for API 1"),                
                
                new ApiScope("api2.read", "Read permission for API 2"),
                new ApiScope("api2.write", "Write permission for API 2"),
                new ApiScope("api2.update", "Update permission for API 2"),
                new ApiScope("api2.delete", "Delete permission for API 2"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Client1",
                    ClientName = "Client 1 Api Application",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1.read" }
                },
                new Client
                {
                    ClientId = "Client2",
                    ClientName = "Client 2 Api Application",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api1.read", "api2.write", "api2.update" }
                },
                new Client
                {
                    ClientId = "Client1MVC",
                    ClientName = "Client 1 MVC Application",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    RedirectUris = new string[] { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = new string[] { "https://localhost:5002/signout-callback-oidc" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AbsoluteRefreshTokenLifetime = 60 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RequireConsent = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1.read",
                        "CountryAndCity",
                        "Roles"
                    }
                },
                new Client
                {
                    ClientId = "Client2MVC",
                    ClientName = "Client 2 MVC Application",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    RedirectUris = new string[] { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = new string[] { "https://localhost:5002/signout-callback-oidc" },
                    AccessTokenLifetime = 2 * 60 * 60,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AbsoluteRefreshTokenLifetime = 60 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RequireConsent = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1.read",
                        "CountryAndCity",
                        "Roles"
                    }
                },
                new Client
                {
                    ClientId = "AngularClient",
                    RequireClientSecret = false,
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1.read",
                        "CountryAndCity",
                        "Roles"
                    },
                    RedirectUris = { "http://localhost:4200/callback" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                { 
                    Name = "CountryAndCity", 
                    DisplayName = "Country And City", 
                    Description = "Kullan�c�n�n �lke Ve �ehir Bilgisi",
                    UserClaims = new[] { "country", "city" }
                },
                new IdentityResource
                {
                    Name = "Roles",
                    DisplayName = "Roles",
                    Description = "User Roles",
                    UserClaims = new[] 
                    { 
                        "role"
                    }
                }
            };
        }

        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "vedatermis",
                    Password = "password",
                    Claims = new List<Claim> 
                    { 
                        new Claim("given_name", "Vedat"), 
                        new Claim("family_name", "ERMIS"),
                        new Claim("country", "Turkey"),
                        new Claim("city", "�stanbul"),
                        new Claim("role", "admin"),
                    } 
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "ayazermis",
                    Password = "password",
                    Claims = new List<Claim> 
                    { 
                        new Claim("given_name", "Ayaz"), 
                        new Claim("family_name", "ERMIS"), 
                        new Claim("country", "Turkey"),
                        new Claim("city", "�stanbul"),
                        new Claim("role", "user"),
                    } 
                }
            };
        }
    }
}