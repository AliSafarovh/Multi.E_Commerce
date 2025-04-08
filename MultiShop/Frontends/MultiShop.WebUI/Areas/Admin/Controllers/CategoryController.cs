using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    //[AllowAnonymous]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Kateqoriyalar";
            ViewBag.v3 = "Kateqoriya siyahısı";
            ViewBag.v4 = "Kateqoriya Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [Route("CreateCategory")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Kateqoriyalar";
            ViewBag.v3 = "Yeni Kateqoriya Girişi";
            ViewBag.v4 = "Kateqoriya Əməliyyatları";
            return View();
        }

        [Route("CreateCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // data haqqında məlumat
            var responseMessage = await client.PostAsync("http://localhost:7030/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("http://localhost:7030/api/Categories?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View();
        }
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Kateqoriyalar";
            ViewBag.v3 = "Kateqoriya Güncəlləmı Səhifəsi";
            ViewBag.v4 = "Kateqoriya Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Categories/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // data haqqında məlumat
            var responseMessage = await client.PutAsync("http://localhost:7030/api/Categories/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });

            }
            return View();
        }
    }
}
