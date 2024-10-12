using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class GoThrowDbContext: DbContext
    {
        public GoThrowDbContext(DbContextOptions<GoThrowDbContext> options) : base(options)
        {

        }
        public DbSet<Airport> Airports { get; set; }
    }
}
