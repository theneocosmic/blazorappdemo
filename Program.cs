using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using blazorappdemo;
using System.Globalization;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();
var apiUrl = builder.Configuration.GetValue<string>("apiUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();

await builder.Build().RunAsync();
