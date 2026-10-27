var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(o => o.SerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

var data = builder.Configuration.Get<PortfolioData>() ?? throw new InvalidOperationException("Missing portfolio config");

if (
    data.AccessKeys.Count() == 0 ||
    string.IsNullOrWhiteSpace(data.Profile.Email)
  ) throw new InvalidOperationException("Missing portfolio config");

app.MapGet("/profile", (string key) => {
  var accessKey = data.AccessKeys.FirstOrDefault(k => k.Key == key);

  if (accessKey is null || accessKey.ExpiresAt <= DateOnly.FromDateTime(DateTime.Now)) {
    return Results.Unauthorized();
  }

  return Results.Ok(data.Profile);
});

app.Run();
