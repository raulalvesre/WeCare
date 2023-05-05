using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RazorLight.Compilation;
using RazorLight.Extensions;
using WeCare.API.Auth;
using WeCare.API.Extensions;
using WeCare.API.Filters;
using WeCare.Application;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.Services;
using WeCare.Application.Validators;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var allowSpecificOrigins = "_allowSpecificOrigins";

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Configuration.AddEnvironmentVariables(x => x.Prefix = "WECARE__");


var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("jwt-secret"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMIN", policy => policy.RequireRole("ADMIN"));
    options.AddPolicy("CANDIDATE", policy => policy.RequireRole("CANDIDATE"));
    options.AddPolicy("INSTITUTION", policy => policy.RequireRole("INSTITUTION"));
});

builder.Services.AddControllers(opts => { opts.Filters.Add(typeof(ExceptionFilter)); });

builder.Services.AddCors((corsOptions) =>
{
    corsOptions.AddPolicy(
        name: allowSpecificOrigins,
        policy =>
        {
            // To use ALLOWED_CORS_URLS is needed to pass it as an environment variable
            // Example:
            // env ALLOWED_CORS_URLS="https://www.google.com;http://www.google.com"

            var allowedCorsUrls = Environment.GetEnvironmentVariable("ALLOWED_CORS_URLS");
            if (!string.IsNullOrWhiteSpace(allowedCorsUrls))
            {
                if (allowedCorsUrls.Contains(';'))
                {
                    policy
                        .WithOrigins(allowedCorsUrls.Split(';'))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
                else
                {
                    policy
                        .WithOrigins(new string[] { allowedCorsUrls })
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            }
            else
            {
                // Specific to localhost
                policy
                    .SetIsOriginAllowed(url =>
                        url.StartsWith("http://localhost") || url.StartsWith("https://localhost")
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>()!;
builder.Services.AddSingleton(emailSettings);

builder.Services.AddDbContext<WeCareDatabaseContext>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<CandidateRepository>();
builder.Services.AddScoped<InstitutionRepository>();
builder.Services.AddScoped<ConfirmationTokenRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<VolunteerOpportunityRepository>();
builder.Services.AddScoped<OpportunityCauseRepository>();
builder.Services.AddScoped<OpportunityRegistrationRepository>();
builder.Services.AddScoped<OpportunityInvitationRepository>();
builder.Services.AddScoped<CandidateQualificationRepository>();

builder.Services.AddScoped<VolunteerOpportunityFormValidator>();
builder.Services.AddScoped<CandidateAdminFormValidator>();
builder.Services.AddScoped<CandidateFormValidator>();
builder.Services.AddScoped<InstitutionAdminFormValidator>();
builder.Services.AddScoped<InstitutionFormValidator>();
builder.Services.AddScoped<OpportunityInvitationFormValidator>();

builder.Services.AddScoped<CandidateMapper>();
builder.Services.AddScoped<InstitutionMapper>();
builder.Services.AddScoped<VolunteerOpportunityMapper>();
builder.Services.AddScoped<OpportunityCauseMapper>();
builder.Services.AddScoped<OpportunityRegistrationMapper>();
builder.Services.AddScoped<OpportunityInvitationMapper>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddFluentEmail(emailSettings.FromEmail, emailSettings.DisplayName)
    .AddRazorRenderer()
    .AddSmtpSender(emailSettings.Host, emailSettings.Port, emailSettings.Mail, emailSettings.Password);

builder.Services.AddScoped<CandidateService>();
builder.Services.AddScoped<InstitutionService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ConfirmationTokenService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<VolunteerOpportunityService>();
builder.Services.AddScoped<OpportunityCauseService>();
builder.Services.AddScoped<OpportunityRegistrationService>();
builder.Services.AddScoped<OpportunityInvitationService>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(allowSpecificOrigins);

app.Run();

public partial class Program
{
}