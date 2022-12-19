using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.Authority = "https://localhost:7122";  //Access tokený yayýnlayan kim? tokenýn sahibi kim oýnu veriyoruz. Burdan gidip public key alýyor API
        opts.Audience = "resource_api1";  //ekstra güvenlik için kullanýyoruz.  
    });//Ayný þema ismini vermek gerekiyor
//Þema Instance tutar. iki üyelik sitemimiz varsa(bayi üyelik, normal üyelik) birbirinden ayýrmak için kullanýlýr.


builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ReadProduct", policy =>
    {
        policy.RequireClaim("scope", "api1.read");
    });

    opts.AddPolicy("UpdateOrCreate", policy =>
    {
        policy.RequireClaim("scope", new[] { "api1.update", "api1.create" });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //kimlik doðrulama
app.UseAuthorization();  //yetkiilendirme 

app.MapControllers();

app.Run();
