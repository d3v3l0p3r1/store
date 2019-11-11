using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    public static class IdentityConfig
    {

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("Api1", "Api1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "js",
                    ClientName = "Js client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials
                }
            };
        }

    }
}
