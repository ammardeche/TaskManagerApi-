using TaskApi.Configurations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddAppServicesConfig();
builder.AddApiConfig()
.AddDbContextConfig()
.AddIdentityConfig()
.AddJwtConfig();

var google = builder.Configuration.GetSection("Authentication:Google");
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = google["ClientId"]!;
        options.ClientSecret = google["ClientSecret"]!;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// app.UseMiddleware<ExeptionMiddlware>();
app.MapControllers();


app.Run();