using Microsoft.EntityFrameworkCore;
using OpenMicNight.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMicNight.Data
{
    //adding a context class in order to build a data model to drive the persistence of my domain classes into the sql server 
    public class PerformanceContext:DbContext
    {
        public DbSet<Performance> Performance { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\ProjectModels; Initial Catalog=OpenMicNightData");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Performance>().ToTable("Performance");
            modelBuilder.Entity<Music>().ToTable("Music");
            modelBuilder.Entity<Song>().ToTable("Song");
        }
    }
}
