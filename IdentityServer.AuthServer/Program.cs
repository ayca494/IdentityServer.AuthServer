using IdentityServer.AuthServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
.AddInMemoryApiResources(Config.GetApiResources())
.AddInMemoryApiScopes(Config.GetApiScopes())
.AddInMemoryClients(Config.GetClients())
.AddDeveloperSigningCredential(); //Uygulama sýrasýnda public ve private key proje içinde tutuyor

//builder.Services.AddAuthentication(JwtBearerDefaults.)





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
