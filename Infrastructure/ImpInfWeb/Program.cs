using ImpInfCommon.ApiServices;
using ImpInfFrontCommon;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components.Authorization;
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

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();


await builder.Build().RunAsync();