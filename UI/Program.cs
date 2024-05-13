using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Data.Repositories.EntityFramework;
using Data;
using UI.Utilities;
using Data.Repositories.FromApi;
using Data.Repositories.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    // secure anything in the Pages/Admin folder 
    // by assigning it the admin policy
    // which we created below 
    // saying it requires a user to have the admin role
    options.Conventions.AuthorizeFolder("/Admin", AdminHelper.ADMIN_POLICY);
});



builder.Services.AddDbContext<MoStuffContext>(options =>
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")));



builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MoStuffContext>();



// add this section to configure authorization options
builder.Services.AddAuthorization(options =>
{
    // in our authorization options we add a policy
    // that requires the user to have the admin role
    options.AddPolicy(AdminHelper.ADMIN_POLICY, policy =>
    {
        policy.RequireRole(AdminHelper.ADMIN_ROLE);
    });
});



// EF repositories
builder.Services.AddScoped<IStoreItemRepository, StoreItemRepositoryEf>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepositoryEf>();
builder.Services.AddScoped<IOrderRepository, OrderRepositoryEf>();

// API
//builder.Services.AddScoped<IStoreItemRepository, StoreItemRepositoryFromApi>();
//builder.Services.AddHttpClient<IStoreItemRepository, StoreItemRepositoryFromApi>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7130/");
//});

// in memory repository
//builder.Services.AddSingleton<IStoreItemRepository, StoreItemRepositoryMem>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();



// seed admin stuff
using (var scope = app.Services.CreateScope())
{
    await AdminHelper.SeedAdminAsync(scope.ServiceProvider);
}



app.Run();
