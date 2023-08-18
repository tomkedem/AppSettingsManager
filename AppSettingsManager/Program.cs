using AppSettingsManager.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var twilioSettings = new TwilioSettings();
builder.Configuration.GetSection("Twilio").Bind(twilioSettings);
builder.Services.AddSingleton(twilioSettings);
//new ConfigureFromConfigurationOptions<TwilioSettings>(builder.Configuration.GetSection("Twilio").Configure(twilioSettings);

// Configure TwilioSettings
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
