using ImpInfCommon.ApiServices;
using ImpInfFrontCommon;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string backRoot = "http://localhost:8080/api/";

builder.Services.AddTransient(sp => new AuthService(backRoot, "Account"));
builder.Services.AddTransient(async sp => new DaysServices(backRoot, await getCokkieAsync(sp.GetRequiredService<CookieService>())));
builder.Services.AddTransient(async sp =>  new LessonsService(backRoot, await getCokkieAsync(sp.GetRequiredService<CookieService>())));
builder.Services.AddTransient(sp => new NewsService(backRoot));
builder.Services.AddTransient(sp => new NotesService(backRoot));
builder.Services.AddTransient(sp => new UsersService(backRoot));

async Task<string> getCokkieAsync(CookieService service) => await service.ReadCookies("token");
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<CookieService>();


var app = builder.Build();

var cs = app.Services.GetRequiredService<CookieService>();

public class CokkieHelper
{
    public string Token { get; set; } = "zero";
}