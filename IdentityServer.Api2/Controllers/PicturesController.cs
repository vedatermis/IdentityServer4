using System.Collections.Generic;
using IdentityServer.Api2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Api2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PicturesController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetPictures()
        {
            var pictures = new List<Picture>
            {
                new Picture {Id = 1, Name = "Ayaz", Url = "/ayaz.jpg"},
                new Picture {Id = 2, Name = "Merve", Url = "/merve.jpg"},
                new Picture {Id = 3, Name = "Vedat", Url = "/vedat.jpg"},
                new Picture {Id = 4, Name = "Mehmet", Url = "/mehmet.jpg"},
                new Picture {Id = 5, Name = "Duman", Url = "/duman.jpg"}
            };

            return Ok(pictures);
        }
    }
}