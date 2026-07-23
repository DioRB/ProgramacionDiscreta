using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProgramacionDiscreta;
using ProgramacionDiscreta.Src.AlgebraBooleana.SimplificacionBooleana;
using ProgramacionDiscreta.Src.AlgebraBooleana.TablasVerdad;
using ProgramacionDiscreta.Src.Criptografia.Cesar;
using ProgramacionDiscreta.Src.Criptografia.MPC;
using ProgramacionDiscreta.Src.Criptografia.RSA;
using ProgramacionDiscreta.Src.Cuantico;
using ProgramacionDiscreta.Src.Grafos.ColoreoGrafo;
using ProgramacionDiscreta.Src.Grafos.Dijkstra;
using ProgramacionDiscreta.Src.Grafos.ImpactoRed;
using ProgramacionDiscreta.Src.Shannon;
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
builder.Services.AddScoped<IBooleanSimplifierService, BooleanSimplifierService>();
builder.Services.AddScoped<EntropiaShannonService>();
builder.Services.AddScoped<ComparacionShannonService>();
builder.Services.AddScoped<QubitGateService>();
builder.Services.AddScoped<MeasurementService>();
builder.Services.AddScoped<QuantumSimulationService>();




await builder.Build().RunAsync();
