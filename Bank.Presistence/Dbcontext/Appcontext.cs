using bank.Domain.Entities;
using Bank.Application.contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Dbcontext
{
    public class Appcontext :IdentityDbContext<Appuser>
    {

        public Appcontext(DbContextOptions<Appcontext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Disposition>()
                .HasOne<Appuser>()
                .WithMany(u => u.Dispositions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Customer>()
                .HasOne<Appuser>()
                .WithOne()
                .HasForeignKey<Customer>(x => x.UserId);
                    modelBuilder.Entity<RefreshToken>()
                  .HasOne<Appuser>()
                  .WithMany(x => x.RefreshTokens)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Disposition> Dispositions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
