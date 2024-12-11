using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Register HttpClient

// Configure application insight logging
builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: (config) =>
        config.ConnectionString = builder.Configuration.GetConnectionString("ApplicationInsightConnectionString"),
        configureApplicationInsightsLoggerOptions: (options) => { }
    );

builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("globalIntegrationUI", LogLevel.Trace);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
