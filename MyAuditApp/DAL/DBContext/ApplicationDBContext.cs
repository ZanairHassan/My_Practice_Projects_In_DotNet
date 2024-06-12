using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId);
                entity.Property(e => e.ExpenseAmmount).HasColumnType("decimal(28, 2)"); // Specify precision and scale
            });

            // Add configurations for other entities as needed
        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseSubType> ExpenseSubTypes { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<FundType> FundTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<MobileCompany> MobileCompanies { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Year> Years { get; set; }
    }
}
