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

app.UseAuthentication(); 
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
	endpoints.MapMetrics();  // Esse endpoint será acessado pelo Prometheus
});

app.UseApiConfiguration(app.Environment);

app.UseSwaggerConfig();

app.Run();
