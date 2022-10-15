using ImpInfCommon.ApiServices;
using ImpInfWeb;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string backRoot = "http://localhost:8080/api/";

builder.Services.AddTransient(sp => new AuthService(backRoot, "Account"));
builder.Services.AddTransient(sp => new DaysServices(backRoot));
builder.Services.AddTransient(sp => new LessonsService(backRoot));
builder.Services.AddTransient(sp => new NewsService(backRoot));
builder.Services.AddTransient(sp => new NotesService(backRoot));
builder.Services.AddTransient(sp => new UsersService(backRoot));

await builder.Build().RunAsync();
