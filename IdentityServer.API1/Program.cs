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
        opts.Authority = "https://localhost:7122";  //Access token� yay�nlayan kim? token�n sahibi kim o�nu veriyoruz. Burdan gidip public key al�yor API
        opts.Audience = "resource_api1";  //ekstra g�venlik i�in kullan�yoruz.  
    });//Ayn� �ema ismini vermek gerekiyor
//�ema Instance tutar. iki �yelik sitemimiz varsa(bayi �yelik, normal �yelik) birbirinden ay�rmak i�in kullan�l�r.


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

app.UseAuthentication(); //kimlik do�rulama
app.UseAuthorization();  //yetkiilendirme 

app.MapControllers();

app.Run();
