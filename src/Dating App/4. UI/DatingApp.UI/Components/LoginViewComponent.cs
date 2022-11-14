using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Components
{
    public class LoginViewComponent : ViewComponent
    {
        public LoginViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}