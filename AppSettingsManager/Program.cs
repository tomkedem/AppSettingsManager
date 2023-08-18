using AppSettingsManager;
using AppSettingsManager.Models;



var builder = WebApplication.CreateBuilder(args);
Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, builder) => {
        //builder.Sources.Clear();
        builder.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);

        builder.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        builder.AddJsonFile($"customJson.json", optional: true, reloadOnChange: true);
        if (hostingContext.HostingEnvironment.IsDevelopment())
        {
            builder.AddUserSecrets<Program>();
        }

        builder.AddEnvironmentVariables();
        builder.AddCommandLine(args);

    });
// Add services to the container.
builder.Services.AddControllersWithViews();


//new ConfigureFromConfigurationOptions<TwilioSettings>(builder.Configuration.GetSection("Twilio").Configure(twilioSettings);

// Configure TwilioSettings
builder.Services.Configure<SocialLoginSettings>(builder.Configuration.GetSection("SocialLoginSettings"));


builder.Services.AddConfiguration<TwilioSettings>(builder.Configuration, "Twilio");
builder.Services.AddConfiguration<SocialLoginSettings>(builder.Configuration, "SocialLoginSettings");
// Options
builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
// AppSetting Configuration




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
