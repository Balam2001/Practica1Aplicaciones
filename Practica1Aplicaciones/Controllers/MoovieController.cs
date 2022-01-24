using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica1Aplicaciones.Domain.Models;
using Practica1Aplicaciones.Infraestructure.Data;
using Practica1Aplicaciones.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practica1Aplicaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoovieController : ControllerBase
    {
        private readonly MoovieDBContext context;

        public MoovieController(MoovieDBContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("films")]
        public async Task<IQueryable<Moovy>> GetAll()
        {
            var query = await context.Moovies.AsQueryable<Moovy>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        [HttpGet]
        [Route("films/{id:int}")]

        public async Task<Moovy> GetById(int id)
        {
            var query = await context.Moovies.FirstOrDefaultAsync(x => x.IdMoovie == id);
            return query;
        }

        [HttpPost]
        [Route("films")]

        public async Task<int> Create([FromBody] Moovy moovy)
        {
            var Entidad = moovy;
            await context.AddAsync(Entidad);
            var checkpoint = await context.SaveChangesAsync();

            if (checkpoint <= 0)
            {
                throw new Exception("El registro no se ha permitido");
            }
            return Entidad.IdMoovie;
        }

        [HttpPut]
        [Route("films/{id:int}")]

        public async Task<bool> Update(int id, [FromBody] Moovy moovy)
        {
            var Entidad = await GetById(id);

            Entidad.Title = moovy.Title;
            Entidad.Director = moovy.Director;
            Entidad.Gender = moovy.Gender;
            Entidad.Score = moovy.Score;
            Entidad.Rating = moovy.Rating;
            Entidad.PublicationYear = moovy.PublicationYear;

            context.Update(Entidad);
            var checkpoint = await context.SaveChangesAsync();
            return checkpoint > 0;
        }

        [HttpDelete]
        [Route("films/{id:int}")]
        public async Task<bool> Delete(int id)
        {
            var Entidad = await GetById(id);
            context.Remove(Entidad);
            var checkpoint = await context.SaveChangesAsync();
            return checkpoint > 0;
        }

        

        [HttpGet]
        [Route("films/filters")]
        public async Task<IQueryable<Moovy>> GetByFilter([FromBody] Moovy MoovieSearch)
        {
            if (MoovieSearch == null)
            {
                return new List<Moovy>().AsQueryable();
            }
            var query = context.Moovies.AsQueryable();

            if (!string.IsNullOrEmpty(MoovieSearch.Title))
            {
                query = query.Where(x => x.Title.Contains(MoovieSearch.Title));
            }
                
            if (!string.IsNullOrEmpty(MoovieSearch.Director))
            {
                query = query.Where(x => x.Director.Contains(MoovieSearch.Director));
            }
                
            if (MoovieSearch.PublicationYear >= 0)
            {
                query = query.Where(x => x.PublicationYear == MoovieSearch.PublicationYear);
            }
            var search = await query.ToListAsync();
            return search.AsQueryable().AsNoTracking();

        }





    }
}
