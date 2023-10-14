using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CancherksWebApp.Pages.Solicitante
{
    public class PublicasModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }

        public string Message { get; set; }
        public PublicasModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<InstallationSportViewModel> InstallationSportViewModels { get; set; }

        public List<Sport> sports { get; set; }

        public Request request { get; set; }

        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            sports = _context.Sport.ToList();
            Page();

        }
        public JsonResult OnGetLoadInstallationbySport(int idSport)
        {
            var data = _context.InstallationSportViewModels.FromSqlRaw("EXEC spGetInstallationsbySport @idSport={0}", idSport).ToList();
            return new JsonResult(data);
        }
    }
}
