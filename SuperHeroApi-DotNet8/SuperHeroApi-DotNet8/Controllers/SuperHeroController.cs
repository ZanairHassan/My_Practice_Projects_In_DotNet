using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroApi_DotNet8.Entities;
using System.Security.Cryptography;

namespace SuperHeroApi_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heros = new List<SuperHero>
            {
                new SuperHero
                {
                    Id= 1,
                    Name="IronMan",
                    FirstName="Jonny",
                    LastName="Sin",
                    Place="California"
                }
            };
            return Ok(heros);
        }
    }
}
