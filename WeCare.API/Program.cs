using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WeCare.API.Auth;
using WeCare.API.Filters;
using WeCare.Application.Mappers;
using WeCare.Application.Services;
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

builder.Services.AddDbContext<WeCareDatabaseContext>();
builder.Services.AddScoped<CandidateRepository>();
builder.Services.AddScoped<VolunteerOpportunityRepository>();
builder.Services.AddScoped<InstitutionRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CandidateMapper>();
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<VolunteerOpportunityService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }