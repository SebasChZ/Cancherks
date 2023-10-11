using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages.Admin
{
    public class GestionSolcitudesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }
        public string Message { get; set; }
        public GestionSolcitudesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }
        public List<Request> Requests { get; set; } // A�ade esta l�nea


        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList(); // Filtrar por idState == 1
        }

    }
}
