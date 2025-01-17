using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Kateqoriyalar";
            ViewBag.v3 = "Kateqoriya siyahısı";
            @ViewBag.v0 = "Kateqoriya Əməliyyatları";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Categories");
            if (responseMessage.IsSuccessStatusCode) 
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values=JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                //Serialize(Metinden-Json) post-put 
                //Deserialize(Json-Metin) Getlist,(id)
                return View(values);
            }
            return View();
        }
    }
}
