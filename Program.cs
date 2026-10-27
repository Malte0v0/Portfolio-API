var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

var data = builder.Configuration.Get<PortfolioData>();

app.MapGet("/profile", (string key) => {
  if (data is null)
  {
    throw new InvalidOperationException("Missing portfolio config");
  }

  var accessKey = data.AccessKeys.FirstOrDefault(k => k.Key == key);

  if (accessKey is null || accessKey.ExpiresAt <= DateOnly.FromDateTime(DateTime.Now)) {
    return Results.Unauthorized();
  }

  return Results.Ok(data.Profile);
});

app.Run();
