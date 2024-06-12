﻿// <auto-generated />
using System;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Configuration", b =>
                {
                    b.Property<int>("ConfigurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConfigurationId"));

                    b.Property<string>("ConfigKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConfigurationId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("DAL.Models.ExceptionLog", b =>
                {
                    b.Property<int>("ExceptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExceptionId"));

                    b.Property<string>("Logtext")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("ExceptionId");

                    b.ToTable("ExceptionLogs");
                });

            modelBuilder.Entity("DAL.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<decimal>("ExpenseAmmount")
                        .HasColumnType("decimal(28, 2)");

                    b.Property<int>("ExpenseCreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpenseCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseDeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpenseDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpenseSubTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ExpenseUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpenseUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("DAL.Models.ExpenseSubType", b =>
                {
                    b.Property<int>("ExpenseSubTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseSubTypeId"));

                    b.Property<DateTime?>("ExpenseSubTypeDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseSubTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseSubTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ExpenseSubTypeId");

                    b.ToTable("ExpenseSubTypes");
                });

            modelBuilder.Entity("DAL.Models.ExpenseType", b =>
                {
                    b.Property<int>("ExpenseTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseTypeId"));

                    b.Property<DateTime?>("ExpenseTypeDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpenseTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ExpenseTypeId");

                    b.ToTable("ExpenseTypes");
                });

            modelBuilder.Entity("DAL.Models.Fund", b =>
                {
                    b.Property<int>("FundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FundId"));

                    b.Property<int>("FundTypeCreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FundTypeCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FundTypeDeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FundTypeDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FundTypeId")
                        .HasColumnType("int");

                    b.Property<int>("FundTypeUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FundTypeUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("FundId");

                    b.ToTable("Funds");
                });

            modelBuilder.Entity("DAL.Models.FundType", b =>
                {
                    b.Property<int>("FundTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FundTypeId"));

                    b.Property<DateTime?>("FundTypeDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FundTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FundTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("FundTypeId");

                    b.ToTable("FundTypes");
                });

            modelBuilder.Entity("DAL.Models.Income", b =>
                {
                    b.Property<int>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeId"));

                    b.Property<int>("IncomeCreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("IncomeCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IncomeDeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("IncomeDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IncomeTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IncomeUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("IncomeUpdatedDated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("IncomeId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("DAL.Models.IncomeType", b =>
                {
                    b.Property<int>("IncomeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeTypeId"));

                    b.Property<string>("IncomeTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IncomeTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("IncomeTypeId");

                    b.ToTable("IncomeTypes");
                });

            modelBuilder.Entity("DAL.Models.Insurance", b =>
                {
                    b.Property<int>("InsuranceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsuranceId"));

                    b.Property<int>("InsuranceCreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsuranceCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InsuranceDeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsuranceDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsuranceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsuranceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InsuranceUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("InsuranceUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("InsuranceId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("DAL.Models.Investment", b =>
                {
                    b.Property<int>("InvestmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvestmentId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvestMentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvestmentDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Investmentdeleted")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InvestmentId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("DAL.Models.MobileCompany", b =>
                {
                    b.Property<int>("MobileCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MobileCompanyId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MobileCompanyCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyPostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileCompanyRegion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("MobileCompanyId");

                    b.ToTable("MobileCompanies");
                });

            modelBuilder.Entity("DAL.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModuleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModuleId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("DAL.Models.ModulePermission", b =>
                {
                    b.Property<int>("ModulePermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModulePermissionId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ModulePermissionId");

                    b.ToTable("ModulePermissions");
                });

            modelBuilder.Entity("DAL.Models.Month", b =>
                {
                    b.Property<int>("MonthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MonthId"));

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MonthId");

                    b.ToTable("Months");
                });

            modelBuilder.Entity("DAL.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<DateTime>("LastRequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RequestId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DAL.Models.Software", b =>
                {
                    b.Property<int>("SoftwareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoftwareId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MonthId")
                        .HasColumnType("int");

                    b.Property<string>("SoftwareDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoftwareName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoftwareType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearId")
                        .HasColumnType("int");

                    b.HasKey("SoftwareId");

                    b.ToTable("Softwares");
                });

            modelBuilder.Entity("DAL.Models.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"));

                    b.Property<DateTime?>("Createddate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("JwtToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TokenId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeletedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("createdBy")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.Models.UserTypes", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypeId"));

                    b.Property<bool>("Isdeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("DAL.Models.Year", b =>
                {
                    b.Property<int>("YearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("YearId"));

                    b.Property<string>("YearName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YearId");

                    b.ToTable("Years");
                });
#pragma warning restore 612, 618
        }
    }
}
