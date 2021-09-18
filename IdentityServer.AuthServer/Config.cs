using System.Collections.Generic;
using IdentityServer4.Models;

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
                }
            };
        }
    }
}