using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Components
{
    [ViewComponent]
    public class NavbarViewComponent : ViewComponent
    {
        public NavbarViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
