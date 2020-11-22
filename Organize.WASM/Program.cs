using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Organize.Shared.Contracts;
using Organize.Business;
using Organize.TestFake;
using Organize.WASM.ItemEdit;
using Organize.DataAccess;
using Organize.WASM.OrganizeAuthenticationStateProvider;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using GeneralUI.BusyOverlay;
using Blazored.Modal;

namespace Organize.WASM
{
    public class Program
    {
        private static bool _isApiPersistance = true;

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddOptions();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddBlazoredModal();

            builder.Services.AddScoped<BusyOverlayService>();

            if (_isApiPersistance)
            {
                builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["apiAddress"]) });

                builder.Services.AddScoped<IPersistanceService, WebAPIAccess.WebAPIAccess>();

                builder.Services.AddScoped<IUserDataAccess, WebAPIAccess.WebAPIUserDataAccess>();

                builder.Services.AddScoped<WebAPIAuthenticationStateProvider>();

                builder.Services.AddScoped<IAuthenticationStateProvider>(
                    provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());

                builder.Services.AddScoped<AuthenticationStateProvider>(
                    provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());
            }
            else
            {
                builder.Services.AddScoped<IPersistanceService, InMemoryStorage.InMemoryStorage>();

                //builder.Services.AddScoped<IPersistanceService, IndexedDB.IndexedDB>();

                builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();

                builder.Services.AddScoped<SimpleAuthenticationStateProvider>();

                builder.Services.AddScoped<IAuthenticationStateProvider>(
                    provider => provider.GetRequiredService<SimpleAuthenticationStateProvider>());

                builder.Services.AddScoped<AuthenticationStateProvider>(
                    provider => provider.GetRequiredService<SimpleAuthenticationStateProvider>());
            }

            builder.Services.AddScoped<IUserManager, UserManager>();

            //.Services.AddScoped<IUserManager, UserManagerFake>();

            builder.Services.AddScoped<IUserItemManager, UserItemManager>();

            builder.Services.AddScoped<IItemDataAccess, ItemDataAccess>();

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

            builder.Services.AddScoped<ItemEditService>();

            //await builder.Build().RunAsync();

            var host = builder.Build();

            var persistanceService = host.Services.GetRequiredService<IPersistanceService>();
            await persistanceService.InitAsync();

            var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();
            var userItemManager = host.Services.GetRequiredService<IUserItemManager>();
            var userManager = host.Services.GetRequiredService<IUserManager>();

            if (persistanceService is InMemoryStorage.InMemoryStorage)
            {
                TestData.CreateTestUser(userItemManager, userManager);
                currentUserService.CurrentUser = TestData.TestUser;
            }

            await host.RunAsync();

        }
    }
}
