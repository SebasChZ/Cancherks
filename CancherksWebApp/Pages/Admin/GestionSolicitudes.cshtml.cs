using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages.Admin
{
    public class GestionSolcitudesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public GestionSolcitudesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }

        public void OnGet()
        {
            Installations = _context.Installation.ToList();
        }
    }
}
