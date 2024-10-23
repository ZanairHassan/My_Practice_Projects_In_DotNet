
using AuditApp;
using BLL.Interfaces;
using BLL.Services;
using DAL.DBContext;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtoption =>
{
    var key = builder.Configuration.GetValue<string>("Jwt:SecretKey");
    var keybyte = Encoding.UTF8.GetBytes(key);
    jwtoption.SaveToken = true;
    jwtoption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keybyte),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AuditAppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuditConnectionString"), x => x.MigrationsAssembly("AuditApp")));
builder.Services.AddTransient<SymmetricCryptographyUtility>();
builder.Services.AddTransient<AsymmetricCryptographyUtility>();
//builder.Services.AddTransient<LoggingUtlity>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddScoped<IFundService,FundService>();
builder.Services.AddScoped<IFundTypeService, FundTypeService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IIncomeTypeService, IncomeTypeService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<IInvestmentTypeService, InvestmentTypeService>();
builder.Services.AddScoped<IMobileCompanyService, MobileCompanyService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IModulePermissionService, ModulePermissionService>();
builder.Services.AddScoped<ISoftwareService, SoftwareService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTypesService,UserTypesService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IAllowedURLService, AllowedURLService>();

builder.Services.AddScoped<AuthenticateURL>();
builder.Services.AddScoped<AuthenticateToken>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
