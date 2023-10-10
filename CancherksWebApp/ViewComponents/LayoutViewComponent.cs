using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CancherksWebApp.ViewComponents
{
    public class LayoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Retrieve the user role from the session
            var userRole = HttpContext.Session.GetString("rol");

            // Determine which layout to use based on the user role
            var layout = "_Layout";
            if (userRole == "2569")
            {
                layout = "_Layout";
            }
            else if (userRole == "7415")
            {
                layout = "_RequesterLayout";
            }

            // Return the appropriate layout
            return View(layout);
        }
    }
}
