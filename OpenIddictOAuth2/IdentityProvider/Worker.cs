using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityServer;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        // Remove existing client to ensure fresh registration
        // var existingClient = await manager.FindByClientIdAsync("console");
        // if (existingClient != null)
        // {
        //     await manager.DeleteAsync(existingClient);
        // }
        // existingClient = await manager.FindByClientIdAsync("client-app");
        // if (existingClient != null)
        // {
        //     await manager.DeleteAsync(existingClient);
        // }

        // if (await manager.FindByClientIdAsync("console") == null)
        // {
        //     await manager.CreateAsync(new OpenIddictApplicationDescriptor
        //     {
        //         ClientId = "console",
        //         ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
        //         DisplayName = "My client application",
        //         Permissions =
        //         {
        //             Permissions.Endpoints.Token,
        //             Permissions.GrantTypes.ClientCredentials
        //         }
        //     });
        // }

        if (await manager.FindByClientIdAsync("client-app") == null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "client-app",
                ClientSecret = "secret",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "protected-api",
                }
            });
        }

        // Register the scope if not already present
        var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

        // var existingScope = await scopeManager.FindByNameAsync("protected-api");
        // if (existingScope != null)
        // {
        //     await scopeManager.DeleteAsync(existingScope);
        // }

        if (await scopeManager.FindByNameAsync("protected-api") == null)
        {
            await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
            {
                Name = "protected-api",
                Description = "Access to the protected API"
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}