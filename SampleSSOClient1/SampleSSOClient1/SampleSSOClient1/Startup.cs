using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using SampleSSOClient1.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;

[assembly: OwinStartup(typeof(SampleSSOClient1.Startup))]

namespace SampleSSOClient1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "empactisidentitytestclient",
                Authority = Constants.BaseAddress,
                RedirectUri = "http://empactisidentitytestclient.azurewebsites.net",
                PostLogoutRedirectUri = "http://empactisidentitytestclient.azurewebsites.net",
                ResponseType = "id_token",
                Scope = "openid all_claims",

                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var identity = n.AuthenticationTicket.Identity;

                        var fullName = identity.FindFirst(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Name);
                        var id = identity.FindFirst(Constants.ClaimTypes.Id);
                        var roles = identity.FindAll(Constants.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity(identity.AuthenticationType,
                            Constants.ClaimTypes.Name,
                            Constants.ClaimTypes.Role);

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        nid.AddClaim(id);
                        nid.AddClaim(fullName);

                        if (roles != null)
                        {
                            nid.AddClaims(roles);
                        }

                        n.AuthenticationTicket = new AuthenticationTicket(nid, n.AuthenticationTicket.Properties);
                    },
                    RedirectToIdentityProvider = async n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                            n.ProtocolMessage.IdTokenHint = idTokenHint;
                            n.ProtocolMessage.ClientId = "empactisidentitytestclient";
                        }
                    }
                }
            });
        }
    }
}