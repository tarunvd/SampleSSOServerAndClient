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

using Microsoft.Owin;

using SampleSSOServer2;

[assembly: OwinStartup("LocalTest", typeof(Startup))]

namespace SampleSSOServer2
{
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    using SampleSSOServer2.Config;
    using SampleSSOServer2.IdentityServer;

    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Logging;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map("/core", coreApp =>
                {
                    ////var factory = new IdentityManagerFactory();
                    ////coreApp.UseIdentityManager(new Thinktecture.IdentityManager.IdentityManagerConfiguration()
                    ////{
                    ////    IdentityManagerFactory = factory.Create
                    ////});

                    var idsrvOptions = new IdentityServerOptions
                    {
                        IssuerUri = "https://idsrv3.com",
                        SiteName = "Sample SSO Server with custom user store",
                        Factory = Factory.Configure(),
                        SigningCertificate = Cert.Load(),

                        CorsPolicy = CorsPolicy.AllowAll,
                        
                        CspOptions = new CspOptions 
                        {
                            ReportEndpoint = EndpointSettings.Enabled,
                        },
                        
                        AuthenticationOptions = new AuthenticationOptions 
                        {
                            IdentityProviders = ConfigureIdentityProviders,
                        }
                    };

                    coreApp.UseIdentityServer(idsrvOptions);
                });

            // only for showing the getting started index page
            app.UseStaticFiles();
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            ////Uncomment to provide external logins
            
            ////var google = new GoogleOAuth2AuthenticationOptions
            ////{
            ////    AuthenticationType = "Google",
            ////    Caption = "Google",
            ////    SignInAsAuthenticationType = signInAsType,
                
            ////    ClientId = "767400843187-8boio83mb57ruogr9af9ut09fkg56b27.apps.googleusercontent.com",
            ////    ClientSecret = "5fWcBT0udKY7_b6E3gEiJlze"
            ////};
            ////app.UseGoogleAuthentication(google);

            ////var fb = new FacebookAuthenticationOptions
            ////{
            ////    AuthenticationType = "Facebook",
            ////    Caption = "Facebook",
            ////    SignInAsAuthenticationType = signInAsType,
                
            ////    AppId = "676607329068058",
            ////    AppSecret = "9d6ab75f921942e61fb43a9b1fc25c63"
            ////};
            ////app.UseFacebookAuthentication(fb);

            ////var twitter = new TwitterAuthenticationOptions
            ////{
            ////    AuthenticationType = "Twitter",
            ////    Caption = "Twitter",
            ////    SignInAsAuthenticationType = signInAsType,
                
            ////    ConsumerKey = "N8r8w7PIepwtZZwtH066kMlmq",
            ////    ConsumerSecret = "df15L2x6kNI50E4PYcHS0ImBQlcGIt6huET8gQN41VFpUCwNjM"
            ////};
            ////app.UseTwitterAuthentication(twitter);

            ////var adfs = new WsFederationAuthenticationOptions
            ////{
            ////    AuthenticationType = "adfs",
            ////    Caption = "ADFS",
            ////    SignInAsAuthenticationType = signInAsType,

            ////    MetadataAddress = "https://adfs.leastprivilege.vm/federationmetadata/2007-06/federationmetadata.xml",
            ////    Wtrealm = "urn:idsrv3"
            ////};
            ////app.UseWsFederationAuthentication(adfs);

            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,

                Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705",
                ClientId = "65bbbda8-8b85-4c9d-81e9-1502330aacba",
                RedirectUri = "https://localhost:44333/core/aadcb",
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
    }
}