using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi_DotNet8.Data;
using SuperHeroApi_DotNet8.Entities;
using System.Security.Cryptography;

namespace SuperHeroApi_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            //var heros = new List<SuperHero>
            //{
            //    new SuperHero
            //    {
            //        Id= 1,
            //        Name="IronMan",
            //        FirstName="Jonny",
            //        LastName="Sin",
            //        Place="California"
            //    }
            //};
            var heros=await _context.SuperHeroes.ToListAsync();
            return Ok(heros);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Gethero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if(hero is null)
            {
                return BadRequest("the hero didn't exist in Server");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updateHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(updateHero.Id);
            if(dbHero is null)
            {
                return NotFound("hero not found");
            }
            dbHero.Name=updateHero.Name;
            dbHero.FirstName=updateHero.FirstName;
            dbHero.LastName=updateHero.LastName;
            dbHero.Place=updateHero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero is null)
            {
                return NotFound("hero not found");
            }
            _context.SuperHeroes.Remove(dbHero);
          

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
