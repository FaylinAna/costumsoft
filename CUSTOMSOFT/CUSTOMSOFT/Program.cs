
using CUSTOMSOFT.API.Filters;
using CUSTOMSOFT.API.Models;
using CUSTOMSOFT.APLICATION.Queries.Package;
using CUSTOMSOFT.CORE.Interfaces;
using CUSTOMSOFT.INFRAESTRUCTURE;
using CUSTOMSOFT.INFRAESTRUCTURE.Autentication;
using CUSTOMSOFT.INFRAESTRUCTURE.Data;
using CUSTOMSOFT.INFRAESTRUCTURE.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog.Events;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using CUSTOMSOFT.API.Helpers.Interfaces;
using CUSTOMSOFT.API.Helpers.Services;
using CUSTOMSOFT.APLICATION.Commands.Package;

var builder = WebApplication.CreateBuilder(args);

// Logs
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();
builder.Host.UseSerilog();

//CORS
builder.Services.AddCors(options => 
options.AddPolicy("AllowAll", p => p
.WithOrigins("http://localhost:3000")
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()));

//Filters
builder.Services.AddControllers()
    .AddControllersAsServices()
    .ConfigureApplicationPartManager(manager =>
    {
        var filterDescriptor = manager.FeatureProviders
            .OfType<ControllerFeatureProvider>()
            .SingleOrDefault();
        if (filterDescriptor != null)
        {
            manager.FeatureProviders.Remove(filterDescriptor);
        }
    });

builder.Services.AddScoped<ValidateTokenFilter>();

builder.Services.AddControllers()
    .AddMvcOptions(options =>
    {
        options.Filters.Add(new ModelStateValidationFilter()); 
    });
//security
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Size File
//builder.Services.Configure<FormOptions>(options =>
//{
//    options.ValueLengthLimit = 1024 * 1024 * 1024; // 
//    options.MultipartBodyLengthLimit = 1024 * 1024 * 1024; // 
//    options.MultipartHeadersLengthLimit = 1024 * 1024 * 1024; // 
//});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1024 * 1024 * 100;
    options.MultipartHeadersLengthLimit = 1024 * 1024 * 1024;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConnectionSettings>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IFileRepository, FilesRepository>();
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining(typeof(GetPackageByTrackingNumberQuery)));
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining(typeof(AddPackageCommand)));

builder.Services.AddScoped<IFileService, FilesServices>();
var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

;app.UseHttpsRedirection();
app.MapControllers();
//Log;
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();


app.Run();
