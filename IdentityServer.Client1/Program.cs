var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = "Cookies"; //Client1 de olu�acak Cookie tan�ml�yoruz
    opts.DefaultChallengeScheme = "oidc"; //Cookie kiminle haberle�ecek onu belirtiyoruz. Idenetityserverdan gelen openid cookie ile haberle�ecek

}).AddCookie("Cookies").AddOpenIdConnect("oidc", opts =>
{
    opts.SignInScheme = "Cookies";
    opts.Authority = "https://localhost:7122"; //yetkili kim cookie da��tan kim? Identityserver 
    opts.ClientId = "Client1-Mvc"; //identity configde GetClient() ks�m�ndaki tan�mlad���m�z ClientId
    opts.ClientSecret = "secret";
    opts.ResponseType = "code id_token";
});//korunakl� bir sayfaya eri�ilmek istendi�inde Clientid identity ile haberle�erek login sayfas�na y�nlendirecek.


// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
