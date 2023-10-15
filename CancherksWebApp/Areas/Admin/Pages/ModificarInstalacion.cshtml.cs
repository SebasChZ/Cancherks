using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Areas.Admin.Pages
{
    public class ModificarInstalacionModel : PageModel
    {
        public string role { get; set; }
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
        }
    }
}
