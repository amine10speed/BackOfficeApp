using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeApp.Data
{
    using BackOfficeApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class BibliothequeContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Bibliotheque;Trusted_Connection=True;Encrypt=False;");
        }
    }

}
