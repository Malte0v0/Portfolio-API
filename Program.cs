var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

var data = builder.Configuration.Get<PortfolioData>();

app.MapGet("/", () => data);

app.Run();
