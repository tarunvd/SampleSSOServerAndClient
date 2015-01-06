/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace SampleSSOServer2.Config
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