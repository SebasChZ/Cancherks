using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace CancherksWebApp.Pages.Admin
{
    public class ReportesModel : PageModel
    {
        public string role { get; set; }
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
        }
    }
}
