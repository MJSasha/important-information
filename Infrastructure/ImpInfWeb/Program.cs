using ImpInfFrontCommon;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string backRoot = "http://localhost:8080/api";
ServiceBuilder.RegistrateCommonServices(builder.Services, backRoot);
var app = builder.Build().RunAsync();