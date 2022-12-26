using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentAPI.Models;
using System.Security.Cryptography.X509Certificates;

namespace RentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Automobil> Automobils { get; set;}
        public DbSet<Korisnik> Korisniks { get; set;}
        public DbSet<Grad> Grads { get; set;}
        public DbSet<Izdavac> Izdavacs { get; set;}
        public DbSet<Komentar> Komentars { get; set;}
        public DbSet<ModelAutomobila> ModelAutomobilas { get; set;}
        public DbSet<Ocjena> Ocjenas { get; set;}
        public DbSet<Proizvodjac> Proizvodjacs { get; set;}
        public DbSet<Rezervisanje> Rezervisanjes { get; set;}
        public DbSet<TipAutomobila> TipAutomobilas { get; set;}
        public DbSet<TipGoriva> TipGorivas { get; set;}
        public DbSet<TipKorisnika> TipKorisnikas { get; set;}


    }
}
