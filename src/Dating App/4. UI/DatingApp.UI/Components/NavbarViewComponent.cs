using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
