using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WeCare.API.Filters;
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
builder.Services.AddTransient<CandidateRepository>();
builder.Services.AddTransient<VolunteerOpportunityRepository>();
builder.Services.AddTransient<InstitutionRepository>();
builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<VolunteerOpportunityService>();
builder.Services.AddScoped<InstitutionService>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }