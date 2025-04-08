using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AllowAnonymous]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Məhsullar";
            ViewBag.v3 = "Məhsul siyahısı";
            ViewBag.v4 = "Məhsul Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Products");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }

            return View();
        }


        [Route("ProductListWithCategory")]

        public async Task<IActionResult> ProductListWithCategory()
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Məhsullar";
            ViewBag.v3 = "Məhsul siyahısı";
            ViewBag.v4 = "Məhsul Əməliyyatları";

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Products/ProductListWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }

        [Route("CreateProduct")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.v1 = "Ana Səhifə"; 
            ViewBag.v2 = "Məhsullar";
            ViewBag.v3 = "Məhsul siyahısı";
            ViewBag.v4 = "Məhsul Əməliyyatları";

            var client = _httpClientFactory.CreateClient();// IHttpClientFactory interfeysindən bir HTTP müştərisi yaradır. 
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Categories");//Nə edir: HTTP GET sorğusu göndərərək, kateqoriya məlumatlarını istəyir.
            var jsonData = await responseMessage.Content.ReadAsStringAsync();// responseMessage obyektinin içindəki cavabın mətn (JSON) formatında məlumatını oxuyur.
            var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);//JSON-dakı məlumatlar ResultCategoryDto adlı obyektlərə uyğun olaraq deserializə olunur (yəni obyektə çevrilir).
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;//Yuxarıda yaradılan categoryValues siyahısını View-a ötürür.
            return View();//CreateProduct adlı View-u göstərir. Bu View istifadəçiyə məhsul əlavə etmək üçün forma təqdim edir.
        }

        [Route("CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //JSON məlumatını HTTP POST üçün uyğun formatda hazırlayır.
            //Encoding.UTF8: Məlumat UTF-8 formatında kodlanır.
            //"application/json": Məlumatın tipini JSON olaraq göstərir.
            var responseMessage = await client.PostAsync("http://localhost:7030/api/Products", stringContent);//Hazırlanan JSON məlumatını API-yə POST sorğusu ilə göndərir.
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
                //Əgər POST sorğusu uğurlu olarsa, istifadəçi məhsul siyahısına yönləndirilir
            }
            return View();// Əgər sorğu uğursuz olarsa, səhifəni olduğu kimi yenidən göstərir (hər hansı səhv mesajı üçün).
        }
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("http://localhost:7030/api/Products?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
            }
            return View();
        }

        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.v1 = "Ana Səhifə";
            ViewBag.v2 = "Məhsullar";
            ViewBag.v3 = "Məhsul Güncəlləmə Siyahısı";
            ViewBag.v4 = "Məhsul Əməliyyatları";

            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("http://localhost:7030/api/Categories");
            if (responseMessage1.IsSuccessStatusCode)
            {
                var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
                var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);
                ViewBag.CategoryValues = values1
                    .Select(x => new SelectListItem { Text = x.CategoryName, Value = x.CategoryId })
                    .ToList();
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7030/api/Products/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }

            return View();
        }

        [Route("UpdateProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // data haqqında məlumat
            var responseMessage = await client.PutAsync("http://localhost:7030/api/Products/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });

            }
            return View();
        }

    }
}