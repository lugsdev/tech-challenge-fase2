using ContactManagement.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddApiConfiguration(configuration)
    .AddIndentity(configuration)
    .ResolveDependencies(configuration)
    .AddSwaggerConfig();

builder.AddCustomLogging();

//builder.WebHost.ConfigureKestrel(options =>
//{
//	options.ListenAnyIP(8080);
//});

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.Run();
