using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Business;
//using Organize.TestFake;
using OrganizeDotNET6a.WASM.ItemEdit;
using OrganizeDotNET6a.DataAccess;
using OrganizeDotNET6a.WASM.OrganizeAuthenticationStateProvider;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using GeneralUi.BusyOverlay;
using Blazored.Modal;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http;
using System;
using OrganizeDotNET6a.InMemoryStorage;
using OrganizeDotNET6a.WASM;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// https://blazorschool.com/tutorial/blazor-wasm/dotnet5/update-from-dotnet-5-to-dotnet-6-682674
builder.RootComponents.Add<App>("#app"); // added # for dotNET6 
builder.RootComponents.Add<HeadOutlet>("head::after"); // for dotNET6
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// https://docs.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/project-structure
/*
 * Blazor WebAssembly apps also define an entry point in Program.cs. The code looks slightly different. The code is 
 * similar in that it's setting up the app host to provide the same host-level services to the app. The WebAssembly 
 * app host doesn't, however, set up an HTTP server because it executes directly in the browser.
 * 
 In a Blazor WebAssembly app, the Program.cs file defines the root components for the app and where they should be rendered:
 */

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredModal();
builder.Services.AddScoped<BusyOverlayService>();

//if (_isApipersistence)
//{
//    Console.WriteLine(builder.Configuration["apiAdress"]);
//    builder.Services.AddScoped(sp => 
//        new HttpClient { BaseAddress = new Uri(builder.Configuration["apiAddress"]) });
//    builder.Services.AddScoped<IPersistenceService, WebAPIAccess.WebAPIAccess>();
//    builder.Services.AddScoped<IUserDataAccess, WebAPIAccess.WebAPIUserDataAccess>();
//    builder.Services.AddScoped<WebAPIAuthenticationStateProvider>();
//    builder.Services.AddScoped<IAuthenticationStateProvider>(
//        provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());
//    builder.Services.AddScoped<AuthenticationStateProvider>(
//        provider => provider.GetRequiredService<WebAPIAuthenticationStateProvider>());

//}
//else
//{
builder.Services.AddScoped<IPersistenceService, cInMemoryStorage>();
//builder.Services.AddScoped<IPersistenceService, IndexedDB.IndexedDB>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();

builder.Services.AddScoped<SimpleAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthenticationStateProvider>(
    provider => provider.GetRequiredService<SimpleAuthenticationStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider>(
    provider => provider.GetRequiredService<SimpleAuthenticationStateProvider>());
//}


builder.Services.AddScoped<IUserManager, cUserManager>();
//builder.Services.AddScoped<IUserManager, UserManagerFake>();
builder.Services.AddScoped<IUserItemManager, UserItemManager>();

builder.Services.AddScoped<IForItemDataAccess, ItemDataAccess>();


builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<ItemEditService>();

var host = builder.Build();

var persistenceService = host.Services.GetRequiredService<IPersistenceService>();
await persistenceService.InitAsync();

var currentUserService = host.Services.GetRequiredService<ICurrentUserService>();
var userItemManager = host.Services.GetRequiredService<IUserItemManager>();
var userManager = host.Services.GetRequiredService<IUserManager>();

//if (persistenceService is InMemoryStorage.InMemoryStorage)
//{
//    TestData.CreateTestUser(userItemManager, userManager);
//    currentUserService.CurrentUser = TestData.TestUser;
//}

// await host.RunAsync();
await builder.Build().RunAsync();
