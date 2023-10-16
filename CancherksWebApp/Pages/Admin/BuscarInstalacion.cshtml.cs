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

        public List<InstallationSportViewModel> InstallationSportViewModels { get; set; }
        public List<Installation> Installations { get; set; }
        
        public List<Sport> Sports { get; set; }

        public IList<Installation> Installation { get; set; }

        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Installations = _context.Installation.ToList();

        }


        public JsonResult OnGetLoadInstallationbySport(int idSport)
        {
            var data = _context.InstallationSportViewModels.FromSqlRaw("EXEC spGetInstallationsbySport @idSport={0}", idSport).ToList();
            return new JsonResult(data);
        }


    }
}
