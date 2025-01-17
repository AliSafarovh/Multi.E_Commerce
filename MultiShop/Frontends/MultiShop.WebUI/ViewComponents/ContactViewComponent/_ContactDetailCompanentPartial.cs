using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.ContactViewComponent
{
    public class _ContactDetailCompanentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
