using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    [Route("Admin/Feature")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Önə Çıxan Sahələr";
            ViewBag.v3 = "Önə Çıxanların siyahısı";
            ViewBag.v4 = "Önə Çıxan Sahələrin Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Features");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [Route("CreateFeature")]
        [HttpGet]
        public IActionResult CreateFeature()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Önə Çıxan Sahələr";
            ViewBag.v3 = "Önə Çıxanların siyahısı";
            ViewBag.v4 = "Önə Çıxan Sahələrin Əməliyyatları";
            return View();
        }

        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // data haqqında məlumat
            var responseMessage = await client.PostAsync("http://localhost:7030/api/Features", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("http://localhost:7030/api/Features?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Önə Çıxan Sahələr";
            ViewBag.v3 = "Önə Çıxanların siyahısı";
            ViewBag.v4 = "Önə Çıxan Sahələrin Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Features/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFeatureDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:7030/api/Features/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Feature", new { area = "Admin" });

            }
            return View();
        }
    }
}
