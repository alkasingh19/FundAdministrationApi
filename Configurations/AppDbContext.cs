using FundAdministrationApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FundAdministrationApi.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fund> Funds => Set<Fund>();
        public DbSet<Investor> Investors => Set<Investor>();
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}