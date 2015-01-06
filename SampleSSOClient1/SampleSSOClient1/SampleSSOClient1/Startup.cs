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
                ClientId = "SSOClient1",
                Authority = Constants.BaseAddress,
                RedirectUri = "http://localhost:12007/",
                PostLogoutRedirectUri = "http://localhost:12007/",
                ResponseType = "id_token",
                Scope = "openid profile roles",

                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var id = n.AuthenticationTicket.Identity;

                        var fullName = id.FindFirst(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Name);
                        var sub = id.FindFirst(Constants.ClaimTypes.Subject);
                        var roles = id.FindAll(Constants.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity(
                            id.AuthenticationType,
                            Constants.ClaimTypes.Name,
                            Constants.ClaimTypes.Role);

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        nid.AddClaim(fullName);
                        nid.AddClaim(sub);
                        nid.AddClaims(roles);

                        n.AuthenticationTicket = new AuthenticationTicket(nid, n.AuthenticationTicket.Properties);
                    },
                    RedirectToIdentityProvider = async n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token").Value;
                            n.ProtocolMessage.IdTokenHint = idTokenHint;
                            n.ProtocolMessage.ClientId = "SSOClient1";
                        }
                    }
                }
            });
        }
    }
}