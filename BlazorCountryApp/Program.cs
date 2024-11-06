using BlazorCountryApp;
using BlazorCountryApp.Interface;
using CountryData.Standard;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<ICountryDataApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7085"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CountryHelper>();
await builder.Build().RunAsync();
