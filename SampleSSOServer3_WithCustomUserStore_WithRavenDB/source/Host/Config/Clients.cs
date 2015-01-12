
namespace SampleSSOServer3.Config
{
    using System.Collections.Generic;

    using Thinktecture.IdentityServer.Core.Models;

    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Enabled = true,
                 
                    ClientName = "SSO Client 1",
                    ClientId = "SSOClient1",
                    ClientSecret = "secret",
                    Flow = Flows.Implicit,
                    
                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        // SampleSSOClient1
                        "http://localhost:12007/"   
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        // SampleSSOClient1
                        "http://localhost:12007/",
                  
                        // SampleSSOClient2
                        "http://localhost:14264/",
                    },
                    
                    IdentityTokenLifetime = 360,
                    AccessTokenLifetime = 3600
                },
                
                new Client
                {
                    ClientName = "SSO Client 2",
                    Enabled = true,

                    ClientId = "SSOClient2",
                    ClientSecret = "secret",
                    Flow = Flows.Implicit,
                    
                    RequireConsent = true,
                    AllowRememberConsent = true,

                    RedirectUris = new List<string>
                    {
                        // SampleSSOClient2
                        "http://localhost:14264/"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        // SampleSSOClient1
                        "http://localhost:12007/",
                        
                        // SampleSSOClient2
                        "http://localhost:14264/",
                    },
                    
                    IdentityTokenLifetime = 360,
                    AccessTokenLifetime = 3600
                },
            };
        }
    }
}