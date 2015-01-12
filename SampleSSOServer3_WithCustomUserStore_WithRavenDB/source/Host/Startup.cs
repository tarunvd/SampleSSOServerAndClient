using Microsoft.Owin;

using SampleSSOServer3;

[assembly: OwinStartup("Startup", typeof(Startup))]

namespace SampleSSOServer3
{
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    using SampleSSOServer3.Config;

    using Thinktecture.IdentityServer.Core.Configuration;
    using Thinktecture.IdentityServer.Core.Logging;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());

            app.Map("/core", coreApp =>
                {
                    var idsrvOptions = new IdentityServerOptions
                    {
                        IssuerUri = "https://idsrv3.com",
                        SiteName = "Sample SSO Server with custom user store",
                        Factory = Factory.Configure(),
                        SigningCertificate = Certficate.GetCertficate(),

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

            app.UseStaticFiles();
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var openidOptions = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,

                Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705",
                ClientId = "65bbbda8-8b85-4c9d-81e9-1502330aacba",
                RedirectUri = "https://localhost:44333/core/aadcb",
            };

            app.UseOpenIdConnectAuthentication(openidOptions);
        }
    }
}