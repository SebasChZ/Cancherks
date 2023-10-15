using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CancherksWebApp.Areas.Solicitante.Pages
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

        public JsonResult OnGetLoadSchudeuleAviableInfo(int idInstallation)
        {
            var data = _context.ScheduleAvailability.FromSqlRaw("EXEC spGetInstallationAvailabilityFromInstallation @idInstallation={0}", idInstallation).ToList();
            return new JsonResult(data);
        }
    }
}
