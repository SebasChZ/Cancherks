using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages.Admin
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
