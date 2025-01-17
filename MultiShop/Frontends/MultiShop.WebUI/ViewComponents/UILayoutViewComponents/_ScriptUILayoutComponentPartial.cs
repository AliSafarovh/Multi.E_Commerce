using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public partial class Class
    {
        public class _ScriptUILayoutComponentPartial : ViewComponent
        {
            public IViewComponentResult Invoke()
            {
                return View();
            }
        }
    }
}
