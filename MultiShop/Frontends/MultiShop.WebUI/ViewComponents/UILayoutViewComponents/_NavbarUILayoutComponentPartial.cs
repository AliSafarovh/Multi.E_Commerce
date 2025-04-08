using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _NavbarUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string token = "";

            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:5001/connect/token"),
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"client_id","MultiShopVisitorId" },
                        {"client_secret","multishopsecret" },
                        {"grant_type","client_credentials" }
                    })
                };

                using (var response = await httpClient.SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JObject.Parse(content);
                        if (tokenResponse.ContainsKey("access_token"))
                        {
                            token = tokenResponse["access_token"]?.ToString() ?? string.Empty;
                        }
                        else
                        {
                            throw new Exception($"Token tapılmadı! Cavab: {content}");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Token alınmadı! Status: {response.StatusCode}, Cavab: {errorContent}");
                    }
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token boş qayıtdı! Lütfən server tənzimləmələrini yoxlayın.");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await client.GetAsync("http://localhost:7030/api/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }

            return View();
        }
    }
}
