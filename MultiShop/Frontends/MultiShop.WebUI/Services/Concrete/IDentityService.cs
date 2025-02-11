using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IDentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;

        public IDentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,IOptions <ClientSettings> clientSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
        }

        Task<bool> IIdentityService.GetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IIdentityService.SignIn(SignInDto signinDto)
        {
            throw new NotImplementedException();
        }
    }
}
