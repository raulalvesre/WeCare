using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WeCare.API.Auth;
using WeCare.API.Filters;
using WeCare.Application;
using WeCare.Application.Mappers;
using WeCare.Application.Services;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>());
    
builder.Services.AddDbContext<WeCareDatabaseContext>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<CandidateRepository>();
builder.Services.AddScoped<InstitutionRepository>();
builder.Services.AddScoped<ConfirmationTokenRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<VolunteerOpportunityRepository>();
builder.Services.AddScoped<OpportunityCauseRepository>();

builder.Services.AddScoped<CandidateMapper>();
builder.Services.AddScoped<InstitutionMapper>();
builder.Services.AddScoped<VolunteerOpportunityMapper>();

builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<InstitutionService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ConfirmationTokenService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<VolunteerOpportunityService>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

var configuration = app.Configuration;

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }