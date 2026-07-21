using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProgramacionDiscreta;
using ProgramacionDiscreta.Src.AlgebraBooleana.TablasVerdad;
using ProgramacionDiscreta.Src.Criptografia.Cesar;
using ProgramacionDiscreta.Src.Criptografia.MPC;
using ProgramacionDiscreta.Src.Criptografia.RSA;
using ProgramacionDiscreta.Src.Grafos.ColoreoGrafo;
using ProgramacionDiscreta.Src.Grafos.Dijkstra;
using ProgramacionDiscreta.Src.Grafos.ImpactoRed;
using ProgramacionDiscreta.Views.Grafos;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CesarCipher>();
builder.Services.AddScoped<ExtendedEuclidService>();
builder.Services.AddScoped<AritmeticaModularService>();
builder.Services.AddScoped<RSAService>();
builder.Services.AddScoped<SecretSharing>();
builder.Services.AddScoped<MPC>();
builder.Services.AddScoped<GrafoService>();
builder.Services.AddScoped<DijkstraService>();
builder.Services.AddScoped<ImpactoRedService>();
builder.Services.AddScoped<ColoreoGrafoService>();
builder.Services.AddScoped<ValidacionColoreoService>();
builder.Services.AddScoped<ExpresionBoolService>();
builder.Services.AddScoped<GeneradorTablaVerdadService>();


await builder.Build().RunAsync();
