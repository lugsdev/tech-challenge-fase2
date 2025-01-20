using ContactManagement.Api.Configurations;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
	.AddApiConfiguration(configuration)
	.AddIndentity(configuration)
	.ResolveDependencies(configuration)
	.AddSwaggerConfig();

builder.AddCustomLogging();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();  // Esse endpoint será acessado pelo Prometheus
});

app.UseApiConfiguration(app.Environment);

app.UseSwaggerConfig();


// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(8080);
// });

app.Run();
