using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Auth;
using Tasks;

if (Env.Config.ENVIRONMENT == "local")
{
    Shared.Infrastructure.Database.Initialize();
}

static void RegisterEndpoints(WebApplication app)
{
    new AuthEndpoint("auth/v1").Register(app);
    new TasksEndpoint("tasks/v1").Register(app);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.Config.SECRET_KEY));
    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false, // just for testing should be enabled in production
        ValidateIssuer = false, // just for testing should be enabled in production
        IssuerSigningKey = signingKey,
    };
});

var app = builder.Build();

RegisterEndpoints(app);

// This is for dev only, must be specified in production
app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader()
);

app.Run($"http://0.0.0.0:{Env.Config.PORT}");