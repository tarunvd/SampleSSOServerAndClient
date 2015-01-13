
namespace SampleSSOServer4
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Framework.ConfigurationModel;
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    using Config;
    using IdentityServer;

    using Thinktecture.IdentityServer.Core.Configuration;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
            var requireSsl = true;

            #if DEBUG
                        requireSsl = false;
            #endif
            ////app.Map("/core", coreApp =>
            ////{
            var idsrvOptions = new IdentityServerOptions
                {
                    IssuerUri = "https://aqdev01/identityserversamplewithaspvnext",
                    SiteName = "Sample SSO Server with custom user store",
                    Factory = Factory.Configure(),
                    SigningCertificate = Certificate.GetCertificate(),

                    CorsPolicy = CorsPolicy.AllowAll,

                    CspOptions = new CspOptions
                    {
                        ReportEndpoint = EndpointSettings.Enabled,
                    },

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                    },

                    RequireSsl = requireSsl
            };

            app.UseIdentityServer(idsrvOptions);
            ////});

            ////app.UseServices(
            ////    services =>
            ////        {
            ////            services.AddMvc(Configuration);

            ////            services.Configure<MvcOptions>(
            ////                options =>
            ////                    {
            ////                        options.Filters.Add(new RequireHttpsAttribute());
            ////                    });
            ////        });

            app.UseStaticFiles();

            ////app.UseMvc();
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

                ////Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705",
                Authority = "https://aqdev01:35543/identityserversamplewithaspvnext",
                ClientId = "65bbbda8-8b85-4c9d-81e9-1502330aacba",
                ////RedirectUri = "https://localhost:44333/core/aadcb",
                RedirectUri = "https://aqdev01:35543/identityserversamplewithaspvnext/aadcb"
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
    }
}