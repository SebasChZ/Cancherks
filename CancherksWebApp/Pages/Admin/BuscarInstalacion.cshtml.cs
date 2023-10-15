using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CancherksWebApp.Pages.Admin
{
    public class TableModifyModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }

        public TableModifyModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }
        
        public List<Sport> Sports { get; set; }
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Installations = _context.Installation.ToList();

        }
    }
}
