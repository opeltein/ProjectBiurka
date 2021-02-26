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

  





        }
    }


 
}

