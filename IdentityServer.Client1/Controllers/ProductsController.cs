using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer.Client1.Models;
using IdentityServer.Client1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace IdentityServer.Client1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAppHttpClient _appHttpClient;

        public ProductsController(IConfiguration configuration, IAppHttpClient appHttpClient)
        {
            _configuration = configuration;
            _appHttpClient = appHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            //HttpClient httpClient = new HttpClient();

            //var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            //if (disco.IsError)
            //{
            //    //Loglama
            //}

            //var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();
            //clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            //clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            //clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            //TokenResponse token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            //if (token.IsError)
            //{
            //    //Loglama
            //}

            //var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            //httpClient.SetBearerToken(accessToken);

            HttpClient httpClient = await _appHttpClient.GetHttpClient();

            var response = await httpClient.GetAsync("http://localhost:5004/api/products/GetProducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(content);

                return View(products);
            }
            else
            {
                var product = new List<Product>();
                return View(product);
            }
        }
    }
}