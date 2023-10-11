using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages.Solicitante
{
    public class MisReservacionesModel : PageModel
    {
        public string role { get; set; }
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
        }
    }
}
