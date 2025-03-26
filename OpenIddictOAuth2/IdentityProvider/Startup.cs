using IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Linq;

namespace IdentityServer;

public class Startup
{
    public Startup(IConfiguration configuration)
        => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("IDPConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find connection string.");
        }

        #region Comment this when run migration
        // Load the certificate from Azure's built-in certificate store
        var thumbprint = Configuration["CertificateThumbprint"];
        X509Certificate2? certificate = null;

        if (!string.IsNullOrEmpty(thumbprint))
        {
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            certificate = store.Certificates
                .Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false)
                .OfType<X509Certificate2>()
                .FirstOrDefault();
        }

        // Ensure the certificate is loaded
        if (certificate == null)
        {
            throw new InvalidOperationException("Could not find certificate with the given thumbprint.");
        }

        // If not using Azure Certificates
        // Load the .pfx certificate (adjust path and password as needed)
        // var certificate = new X509Certificate2("path-to-your.pfx", "pfx-password");
        #endregion

        services.AddControllersWithViews();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Configure the context to use sqlite.
            options.UseSqlServer(connectionString);

            // Register the entity sets needed by OpenIddict.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            options.UseOpenIddict();
        });

        // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
        // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
        services.AddQuartz(options =>
        {
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>();

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                // Enable the token endpoint.
                options.SetTokenEndpointUris("connect/token");
                    // .SetAuthorizationEndpointUris("connect/authorize")
                    // .SetIntrospectionEndpointUris("connect/introspect");

                // Enable the client credentials flow.
                options.AllowClientCredentialsFlow();

                options.AcceptAnonymousClients();

                // Register the signing and encryption credentials.
                // options.AddDevelopmentEncryptionCertificate()
                //        .AddDevelopmentSigningCertificate();
                #region Comment this when run migration
                // Solution 1: Use a Hardcoded Key (Recommended for Dev/Test)
                // options.AddEncryptionKey(new SymmetricSecurityKey(Convert.FromBase64String("YOUR-SECRET-BASE64-KEY")));
                // options.AddSigningKey(new SymmetricSecurityKey(Convert.FromBase64String("YOUR-SECRET-BASE64-KEY")));

                // Solution 2: Use a PEM Certificate (Recommended for Production) store cert in Azure Certificates
                // options.AddSigningCertificate(certificate);
                // options.AddEncryptionCertificate(certificate); // Best security for Prod env

                // Solution 3: Use Azure Key Vault for Certificates (Best for Production)

                // Solution 4: Custom
                options.AddSigningCertificate(certificate);
                options.AddEphemeralEncryptionKey(); // Use a temporary key to satisfy the requirement
                options.DisableAccessTokenEncryption(); // Still disable encryption for access tokens
                #endregion Comment this when run migration

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableTokenEndpointPassthrough();
                    //    .DisableTransportSecurityRequirement;

                // Register the "protected-api" scope
                options.RegisterScopes("protected-api");
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        // Register the worker responsible for seeding the database.
        // Note: in a real world application, this step should be part of a setup script.
        services.AddHostedService<Worker>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute();
        });

        app.UseWelcomePage();
    }
}
