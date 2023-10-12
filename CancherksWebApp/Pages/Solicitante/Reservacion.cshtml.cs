using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CancherksWebApp.Pages.RequesterUser
{
    public class ReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }

        public ReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<InstallationSportViewModel> InstallationSportViewModels { get; set; }

        public List<Sport> sports { get; set; }

        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");
            sports = _context.Sport.ToList();
            Page();

        }

        public JsonResult OnGetLoadData(int idSport)
        {
            var data = _context.InstallationSportViewModels.FromSqlRaw("EXEC spGetInstallationsbySport @idSport={0}", idSport).ToList();
            return new JsonResult(data);
        }


        public JsonResult OnGetLoadReservationInfo(int idInstallation)
        {
            var data = _context.InstallationSportViewModels.FromSqlRaw("EXEC spGetInstallationsbySport @idSport={0}", idInstallation).ToList();
            return new JsonResult(data);
        }

    }
}
