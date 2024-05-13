using Data;
using Data.Auth;
using Data.Repositories.EntityFramework;
using Domain.Constants;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureApi.Endpoints;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// AUTHORIZATION
var key = Encoding.ASCII.GetBytes(AuthConstants.JWT_SECRET_KEY);
builder.Services.AddAuthentication(configure =>
    configure.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(configure =>
        {
            configure.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

builder.Services.AddAuthorization();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<MoStuffContext>()
        .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoStuffContext>(configure => 
    configure.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IStoreItemRepository, StoreItemRepositoryEf>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthEndpoints();


app.MapStoreItemEndpoints();

app.Run();