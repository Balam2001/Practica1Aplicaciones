using System;
using System.Collections.Generic;
using System.Linq;
using Practica1Aplicaciones.Infraestructure.Data;
using Practica1Aplicaciones.Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Practica1Aplicaciones.Repositories
{
    public class MoovieRepository
    {
        private readonly MoovieDBContext _context;

        public MoovieRepository(MoovieDBContext context)
        {
            this._context = context;
        }

        public async Task<IQueryable<Moovy>> GetAll()
        {
            var query = await _context.Moovies.AsQueryable<Moovy>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

    }
}
