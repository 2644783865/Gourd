using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gourd.IdentityService
{
    /// <summary>
    /// One In-Memory Configuration for IdentityServer => Just for Demo Use
    /// </summary>
    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("AAA", "测试资源A"){ApiSecrets = { new Secret("123456".Sha256()) }},
                new ApiResource("BBB", "测试资源B"){ApiSecrets = { new Secret("123456".Sha256()),new Secret("111111".Sha256()) }},
                new ApiResource("CCC", "测试资源C"){ApiSecrets = { new Secret("123456".Sha256()),new Secret("111111".Sha256()),new Secret("123123".Sha256()) }}
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "webapi",
                    Description = "webapi示例模型",
                    ClientSecrets = new [] { new Secret("123456".Sha256()) },
                    RequireConsent=false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new [] { "AAA" }
                },
                new Client
                {
                    ClientId = "mvc",
                    Description = "mvc示例模型",
                    RequireConsent=false,
                    ClientSecrets = new [] { new Secret("111111".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    RedirectUris = { "http://localhost:5002/signin-oidc","https://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc","https://localhost:5001/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                    },
                    AllowAccessTokensViaBrowser = true // can return access_token to this client
                },
                new Client
                {
                    ClientId = "console",
                    Description = "webapi示例模型",
                    ClientSecrets = new [] { new Secret("123123".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireConsent=false,
                    //Jwt = 0；Reference = 1支持撤销；
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedScopes = new [] { "AAA", "BBB", "CCC" }
                }
        };
    }
    

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "tibos",
                    Password = "Abc@123"
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "admin",
                    Password = "Abc@123"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "test",
                    Password = "Abc@123"
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
