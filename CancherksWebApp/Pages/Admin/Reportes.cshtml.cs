using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace CancherksWebApp.Pages.Admin
{
    public class ReportesModel : PageModel
    {
        public string role { get; set; }
        private readonly ApplicationDbContext _context;

        public ReportesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }

        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Installations = _context.Installation.ToList();

            Page();
        }
    }
}
