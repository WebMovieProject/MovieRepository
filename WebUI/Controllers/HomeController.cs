using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebProject.Models;
using WebUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Dynamic;

namespace MyWebProject.Controllers
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

        public async Task<IActionResult> MainPage(string par="comedy")
        {
            var root = new List<Root1>();
            List<Search> list = new List<Search>();
            dynamic mymodel = new ExpandoObject();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://www.omdbapi.com/?&s={par}&plot=full&apikey=393ccdcc"))
                {
                    var apiResponse = "[" + await response.Content.ReadAsStringAsync() + "]";

                    root = JsonConvert.DeserializeObject<List<Root1>>(apiResponse);
                    foreach (var item in root)
                    {
                        mymodel.List1 = item.Search;
                    }

                   
                }
                using (var response = await httpClient.GetAsync($"http://www.omdbapi.com/?&s=popular&plot=full&apikey=393ccdcc"))
                {
                    var apiResponse = "[" + await response.Content.ReadAsStringAsync() + "]";

                    root = JsonConvert.DeserializeObject<List<Root1>>(apiResponse);
                    foreach (var item in root)
                    {
                        mymodel.List2 = item.Search;
                    }

                   
                }
            }
             return View(mymodel);
        }


        public async Task<IActionResult> GetProductsFromRestApi()
        {
            var root = new List<Root1>();
            List<Search> list = new List<Search>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://www.omdbapi.com/?&s=comedy&plot=full&apikey=393ccdcc"))
                {
                    var apiResponse = "[" + await response.Content.ReadAsStringAsync() + "]";

                    root = JsonConvert.DeserializeObject<List<Root1>>(apiResponse);
                    foreach (var item in root)
                    {
                        list = item.Search;
                    }

                    ViewBag.dat = root;
                    return View(list);
                }
            }

        }

        public async Task<IActionResult> getTeam()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/teams/statistics?league=39&season=2020&team=33"),
                Headers =
    {
        { "x-rapidapi-host", "api-football-v1.p.rapidapi.com" },
        { "x-rapidapi-key", "SIGN-UP-FOR-KEY" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                ViewBag.data = body;
                return View();
            }


        }
        //qewew


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
