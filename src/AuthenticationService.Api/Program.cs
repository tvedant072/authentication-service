using AuthenticationService.Api.Data;
using AuthenticationService.Api.Configuration;
using AuthenticationService.Api.Repositories;
using AuthenticationService.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Authentication Service API",
        Version = "v1",
        Description = "Authentication microservice built with ASP.NET Core Web API."
    });
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddOptions<PasswordHashingOptions>()
    .Bind(builder.Configuration.GetSection(PasswordHashingOptions.SectionName))
    .Validate(
        options => options.WorkFactor is >= 10 and <= 16,
        "PasswordHashing:WorkFactor must be between 10 and 16.")
    .ValidateOnStart();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication Service API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
