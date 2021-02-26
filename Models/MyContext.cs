using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;


namespace ProjectBiurka2.Models
{
    class MyContext : DbContext
    {
        public DbSet<Biurko> Biurka { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Producent> Producenci { get; set; }
        public DbSet<Pomieszczenie> Pomieszczenia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Firma.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Biurko>().ToTable("Biurka", "Biurka");
            modelBuilder.Entity<Pracownik>().ToTable("Pracownicy", "Pracownicy");
            modelBuilder.Entity<Producent>().ToTable("Producenci", "Producenci");
            modelBuilder.Entity<Pomieszczenie>().ToTable("Pomieszczenia", "Pomieszczenia");

            modelBuilder.Entity<Biurko>(entity =>
            {
                entity.HasKey(e => e.BiurkoId);
                entity.HasIndex(e => e.Numer).IsUnique();



            });
            base.OnModelCreating(modelBuilder);





        }
    }

    public class Biurko
    {
        [Key]
        public int BiurkoId { get; set; }
        [Required]
        public int Numer { get; set; }
        [Required]
        public Producent Producent { get; set; }
        [Required]
        public Pomieszczenie Pomieszczenie { get; set; }
        [Required]
        public Pracownik Pracownik { get; set; }
    }


    public class Pomieszczenie
    {
        [Key]
        public int PomieszczenieId { get; set; }
        [Required]
        public string Nazwa { get; set; }


    }

    public class Producent
    {
        [Key]
        public int ProducentId { get; set; }
        [Required]
        public string Nazwa { get; set; }

    }


    public class Pracownik
    {
        [Key]
        public int PracownikId { get; set; }
        [Required]
        public string Imie { get; set; }
        [Required]
        public string Nazwisko { get; set; }

    }
}
