using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Provicers.SQLite
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options) : base(options) { } 
        public DbSet<Excursie> Excursii { get; set; }
        public DbSet<FirmaTransport> FirmeTransport { get; set; }
        public DbSet<ObiectiveTuristice> ObiectiveTuristice { get; set; }
        public DbSet<Persoana> Persoane { get; set; }
        public DbSet<Rezervare> Rezervari { get; set; }
        public DbSet<User> Useri { get; set; }
    }
}
