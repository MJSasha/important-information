using ImpInfCommon.ApiServices;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon;
using ImpInfFrontCommon.Services;
using ImpInfWeb;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string backRoot = "http://localhost:8080/api/";

//builder.Services.AddSingleton<ITokenProvider, TokenProvider>();

builder.Services.AddTransient(sp => new AuthService(backRoot, sp.GetService<TokenProvider>(), "Account"));
builder.Services.AddTransient(sp => new DaysServices(backRoot, sp.GetService<TokenProvider>()));
builder.Services.AddTransient(sp => new LessonsService(backRoot, sp.GetService<TokenProvider>()));
builder.Services.AddTransient(sp => new NewsService(backRoot, sp.GetService<TokenProvider>()));
builder.Services.AddTransient(sp => new NotesService(backRoot, sp.GetService<TokenProvider>()));
builder.Services.AddTransient(sp => new UsersService(backRoot, sp.GetService<TokenProvider>()));
builder.Services.AddTransient(sp => new UsersService(backRoot, sp.GetService<TokenProvider>()));

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
builder.Services.AddScoped<CookieService>();


var app = builder.Build().RunAsync();