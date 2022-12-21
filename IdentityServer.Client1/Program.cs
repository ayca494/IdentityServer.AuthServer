var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = "Cookies"; //Client1 de oluþacak Cookie tanýmlýyoruz
    opts.DefaultChallengeScheme = "oidc"; //Cookie kiminle haberleþecek onu belirtiyoruz. Idenetityserverdan gelen openid cookie ile haberleþecek

}).AddCookie("Cookies").AddOpenIdConnect("oidc", opts =>
{
    opts.SignInScheme = "Cookies";
    opts.Authority = "https://localhost:7122"; //yetkili kim cookie daðýtan kim? Identityserver 
    opts.ClientId = "Client1-Mvc"; //identity configde GetClient() ksýmýndaki tanýmladýðýmýz ClientId
    opts.ClientSecret = "secret";
    opts.ResponseType = "code id_token";
});//korunaklý bir sayfaya eriþilmek istendiðinde Clientid identity ile haberleþerek login sayfasýna yönlendirecek.


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
