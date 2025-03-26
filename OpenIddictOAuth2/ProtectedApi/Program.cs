using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var idp = builder.Configuration["IDP"];

#region
// Load the certificate from Azure's built-in certificate store
// var thumbprint = builder.Configuration["CertificateThumbprint"];
// X509Certificate2? certificate = null;

// if (!string.IsNullOrEmpty(thumbprint))
// {
//     using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
//     store.Open(OpenFlags.ReadOnly);
//     certificate = store.Certificates
//         .Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false)
//         .OfType<X509Certificate2>()
//         .FirstOrDefault();
// }

// // Ensure the certificate is loaded
// if (certificate == null)
// {
//     throw new InvalidOperationException("Could not find certificate with the given thumbprint.");
// }
#endregion

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = idp; // IdP URL
        options.RequireHttpsMetadata = false;
        options.Audience = "protected-api";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            // ValidateIssuer = true,
            // ValidIssuer = idp,
            // ValidateAudience = true,
            // ValidAudience = "protected-api",
            // ValidateLifetime = true,
            // IssuerSigningKey = new X509SecurityKey(certificate),
            // TokenDecryptionKey = new X509SecurityKey(certificate) // Add decryption key
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // If needed, you can add custom validation here
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure middleware
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();