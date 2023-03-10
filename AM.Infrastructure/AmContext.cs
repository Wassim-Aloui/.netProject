using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.ApplicationCore.Domain;
using AM.Infrastructure.Configuration;

namespace AM.Infrastructure
{

    public class AmContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new flightConfiguration());
            modelBuilder.ApplyConfiguration(new planeConfiguration());
            modelBuilder.ApplyConfiguration(new passengerConfiguration());
            modelBuilder.Entity<Flight>().ToTable("MyFlight");
            modelBuilder.Entity<Flight>().HasKey(f=> f.FlightId);
            //modelBuilder.Entity<Passanger>().Property(f=>f.fullName.firstName).HasColumnType("PassengerName")
            //    .HasMaxLength(50)
            //    .IsRequired()
            //    .HasColumnType("varchar");
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(50);
            configurationBuilder.Properties<DateTime>().HaveColumnType("Date");
            configurationBuilder.Properties<Double>().HavePrecision(2,3);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Passanger> Passangers { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //7atina @ 3al \
            optionsBuilder.UseSqlServer(@"data source= (localdb)\mssqllocaldb; initial catalog= wassim; integrated security= true");
        }
    }
}
