using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public async Task<ActionResult> GetRefreshToken()
        {
            var httpClient = new HttpClient();
            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                //Loglama
            }

            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            

            var refreshTokenRequest = new RefreshTokenRequest
            {
                ClientId = _configuration["ClientMvc:ClientId"],
                ClientSecret = _configuration["ClientMvc:ClientSecret"],
                RefreshToken = refreshToken,
                Address = disco.TokenEndpoint
            };
            
            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                //Log
            }

            var tokens = new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = OpenIdConnectParameterNames.IdToken, Value = token.IdentityToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = token.AccessToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = token.RefreshToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture) }
            };

            var authenticationResult = await HttpContext.AuthenticateAsync();
            var properties = authenticationResult.Properties;
            properties.StoreTokens(tokens);

            await HttpContext.SignInAsync("Cookies", authenticationResult.Principal, properties);



            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admina")]
        public IActionResult AdminPage()
        {
            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }
    }
}
