using Entities.Models;
using Entities.Models.Feed;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectX.Models;
using ProjectX.Services.Client;
using System.Diagnostics;

namespace ProjectX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetFeed(int Id)
        {
            var model = new List<FeedResponse>();
            try
            {
                ClientHelper _client = new();
                var json = await _client.CallWebService($"Api/Feed/Get/{Id}", ClientHelper.RequestType.GET);

                model = JsonConvert.DeserializeObject<List<FeedResponse>>(json.ToString());

                if (model is not null)
                    return Json(new { data = model, success = true, message = string.Empty });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "POST: Feed/Get{id} (FeedPageResponse: {0})",
                    JsonConvert.SerializeObject(model));
            }

            //return View();
            return Json(new { data = model, success = false, message = "Não foi possível carregar a home page." });
        }

        [HttpPost]
        public async Task<IActionResult> GetComments(int feedId)
        {
            var model = new List<FeedCommentsResponse>();
            try
            {
                ClientHelper _client = new();
                var json = await _client.CallWebService($"Api/Feed/GetComments/{feedId}", ClientHelper.RequestType.GET);

                model = JsonConvert.DeserializeObject<List<FeedCommentsResponse>>(json.ToString());

                if (model is not null)
                    return Json(new { data = model, success = true, message = string.Empty });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "POST: Feed/GetComments/{feedId} (FeedPageResponse: {0})",
                    JsonConvert.SerializeObject(model));
            }

            //return View();
            return Json(new { data = model, success = false, message = "Não foi possível carregar a home page." });
        }

        [HttpPost]
        public async Task<IActionResult> PostFeed(FeedRequest request)
        {
            var model = new List<FeedResponse>();
            try
            {
                ClientHelper _client = new();
                var json = await _client.CallWebService($"Api/Feed/Post", ClientHelper.RequestType.POST, request);

                model = JsonConvert.DeserializeObject<List<FeedResponse>>(json.ToString());

                if (model is not null)
                    return Json(new { data = model, success = true, message = string.Empty });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "POST: Feed/Post (FeedPageResponse: {0})",
                    JsonConvert.SerializeObject(model));
            }

            //return View();
            return Json(new { data = model, success = false, message = "Não foi possível carregar a home page." });
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