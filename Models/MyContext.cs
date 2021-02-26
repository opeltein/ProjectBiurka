using System;
using System.Collections.Generic;
using System.Text;


namespace Project2.Models
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


 
}

