using BlazorInterAutoProj.Client.Pages;
using BlazorInterAutoProj.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SharpRepoBackendProg.Service;
using System.Web.Services.Description;
using Public01 = SharpSetupProg21Private.AAPublic;

var registration = new Public01.Registration();
registration.Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


var backend = new BackendService();
builder.Services.AddSingleton<BackendService>(backend);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

var gg = app.Services.GetService<BackendService>();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorInterAutoProj.Client._Imports).Assembly);

app.Run();
