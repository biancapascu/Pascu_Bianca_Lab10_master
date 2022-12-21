using gRPCClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Grpc.Net.Client;
using Pascu_Bianca_Lab10;

namespace gRPCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public async Task<IActionResult> Unary()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7294");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SendStatusAsync(new SRequest { No = 3 });
            return View("ShowStatus", (object)ChangetoDictionary(reply));
        }
        private Dictionary<string, string> ChangetoDictionary(SResponse response)
        {
            Dictionary<string, string> statusDict = new Dictionary<string, string>();
            foreach (StatusInfo status in response.StatusInfo)
                statusDict.Add(status.Author, status.Description);
            return statusDict;
        }
    }
}