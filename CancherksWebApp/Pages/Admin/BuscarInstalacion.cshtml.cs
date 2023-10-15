using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public List<Installation> GetInstallationsBySportId(int sportId)
        {
            // Primero, obtenemos los IDs de las instalaciones que coinciden con el idSport proporcionado
            var installationIds = _context.InstallationxSport
                                          .Where(ixs => ixs.IdSport == sportId)
                                          .Select(ixs => ixs.IdInstallation)
                                          .ToList();

            // Luego, obtenemos las instalaciones que coinciden con esos IDs
            var installations = _context.Installation
                                        .Where(i => installationIds.Contains(i.Id))
                                        .ToList();

            return installations;
        }
        [HttpGet]
        public IActionResult GetInstallationsForSport(int sportId)
        {
            var installations = GetInstallationsBySportId(sportId);
            return new JsonResult(installations);
        }




        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Installations = _context.Installation.ToList();

        }
    }
}
