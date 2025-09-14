using BlazorApp1;
using MyBlazorAppSourse.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization; 
using MyBlazorAppSourse.Models;
using MyBlazorAppSourse.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<HttpHandler>();
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<HttpHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler)
    {
        BaseAddress = new Uri(builder.Configuration["DBAddress"]!)
    };
});

builder.Services.AddSingleton<InventoryModel>();
builder.Services.AddSingleton<List<InventoryModel>>();
builder.Services.AddSingleton<ItemModel>();
builder.Services.AddSingleton<List<ItemModel>>();
builder.Services.AddSingleton<List<EditModel>>();
builder.Services.AddSingleton<CurrentUser>();

builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<SalesforceService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<UserService>());

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

await builder.Build().RunAsync();