using Microsoft.AspNetCore.Mvc;

namespace qodeless.presentation.UI.Web.Components
{
    public class MenuUsuario : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
