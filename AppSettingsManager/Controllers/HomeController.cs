using AppSettingsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppSettingsManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private TwilioSettings _twilioSettings;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
           _twilioSettings = config.GetSection("Twilio").Get<TwilioSettings>();
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _config.GetValue<string>("SendGridKey");
            ViewBag.TwilioAuthToken = _config.GetSection("Twilio").GetValue<string>("AuthToken");
            ViewBag.TwilioAccountSid = _config.GetValue<string>("Twilio:AccountSid");
            ViewBag.TwilioPhoneNumber = _twilioSettings.PhoneNumber;
            //ViewBag.BottomLevelSetting = _config.GetValue<string>("FirstLevelSetting:SecondLevelSetting:BottomLevelSetting");
            //ViewBag.BottomLevelSetting = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
            //                            .GetValue<string>("BottomLevelSetting");
            ViewBag.BottomLevelSetting = _config.GetSection("FirstLevelSetting").GetSection("SecondLevelSetting")
                                        .GetSection("BottomLevelSetting").Value;

            return View();
        }

        public IActionResult Privacy()
        {
           
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}