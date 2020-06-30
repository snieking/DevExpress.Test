using System;
using DevExpressGroupingExample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DevExpressGroupingExample.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Person>(entity => { entity.HasKey(e => e.Id); });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var person1 = new Person {Id = 1, FirstName = "Viktor", LastName = "Plane", BirthTime = DateTime.Now};
            var person2 = new Person {Id = 2, FirstName = "Anastasia", LastName = "Zudina", BirthTime = DateTime.Now};

            modelBuilder.Entity<Person>().HasData(person1);
            modelBuilder.Entity<Person>().HasData(person2);
        }
    }
}