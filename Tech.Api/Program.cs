using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tech.Api.Filters;
using Tech.Api.Infrastructure.DataAccess;

const string AUTHENTICATION_TYPE = "Bearer";

var builder = WebApplication.CreateBuilder(args);

// Define o ambiente (Development ou Production)
var env = builder.Environment.EnvironmentName;

// Lê a string de conexão do arquivo correto ou da variável de ambiente
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (env == "Production")
{
    connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    builder.Services.AddDbContext<TechDbContext>(options =>
    options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<TechDbContext>(options =>
    options.UseSqlite(connectionString));
}


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below;
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTHENTICATION_TYPE
                },
                Scheme = "oauth2",
                Name = AUTHENTICATION_TYPE,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKey()


        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

SymmetricSecurityKey SecurityKey()
{
    var signingKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? throw new System.Exception("Chave nao ocnfigurada!");

    var symmetricKey = Encoding.UTF8.GetBytes(signingKey);

    return new SymmetricSecurityKey(symmetricKey);
}