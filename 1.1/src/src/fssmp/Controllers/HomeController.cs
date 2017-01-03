using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fssmp.Controllers
{
    public class HomeController : Controller
    {
        private string clientId = "B0XUU5XHCTMWXYMEFCRNV0AEUZ424QVGPUWX2WY1FQQLDW1C";
        private string clientSecret = "GPLCH5GEC2OXM3AQYAKNMOVOPINHBBM5LMFBSPYVSW4BQW5N";
        private string path = "http://wa-dn.44fs.preview.openshiftapps.com/Home/Foursquare/";
        static List<string> tokens = new List<string>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Foursquare(string code)
        {
            HttpClient client = new HttpClient();
            var task = client.GetAsync(
                $"https://foursquare.com/oauth2/access_token" +
                $"?client_id={clientId}&client_secret={clientSecret}&grant_type=authorization_code" +
                $"&redirect_uri={path}&code={code}");

            var result = task.Result.Content.ReadAsStringAsync().Result;
            var fsResponse = JObject.Parse(result);

            var token = (string)fsResponse.SelectToken("access_token");
            tokens.Add(token);

            return RedirectToAction("Welcome");
        }

        public IActionResult Welcome()
        {
            return View(tokens);
        }
    }

    public class FsResponse
    {
    }
}
