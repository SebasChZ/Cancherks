

using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CancherksWebApp.Pages.RequesterUser
{
    public class ReservationModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }

        public string Message { get; set; }
        public ReservationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<InstallationSportViewModel> InstallationSportViewModels { get; set; }

        public List<Sport> sports { get; set; }

        public Request request { get; set; }  

        public async Task OnGet()
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

        public JsonResult OnGetLoadSchudeuleAviableInfo(int idInstallation, int idDay, string date)
        {
            var data = _context.InstallationAvailabilityViewModels.FromSqlRaw("EXEC spGetInstallationAvailability @idInstallation={0}, @idDay={1}, @date={2}", idInstallation, idDay, date).ToList();
            return new JsonResult(data);
        }


        public async Task<IActionResult> OnPostAsync(string installationId, string dateReservation, string startTimeReservation, string endTimeReservation)
        {
            int idInstallation = Convert.ToInt32(installationId);
            string emailRequester = HttpContext.Session.GetString("email");
            var parameters = new[]
            {
                        new SqlParameter("@idRequest", 0),
                        new SqlParameter("@emailRequester", emailRequester),
                        new SqlParameter("@date", dateReservation),
                        new SqlParameter("@idInstallation", idInstallation),
                        new SqlParameter("@idState", 1),
                        new SqlParameter("@idActivity", 1),
                        new SqlParameter("@startTime", startTimeReservation),
                        new SqlParameter("@endTime", endTimeReservation),
                        new SqlParameter("@operationFlag", 0)
            };
            try
            {
                //execute the sqlRaw
                
                //var data = _context.Installation.FromSqlRaw("EXEC spCrudRequest @idRequest, @emailRequester, @date, @idInstallation, @idState, @idActivity, @startTime, @endTime, @operationFlag", parameters).ToList();
                var chaca =  _context.Database.ExecuteSqlRaw("EXEC dbo.spCrudRequest @idRequest={0}, @emailRequester={1}, @date={2}, @idInstallation={3}, @idState={4}, @idActivity={5}, @startTime={6}, @endTime={7}, @operationFlag={8}",
            0, emailRequester, dateReservation, idInstallation, 1, 1, startTimeReservation, endTimeReservation, 0);
  
                if (chaca == 1)
                {
                    return RedirectToPage("/Solicitante/Reservacion");
                }

            }
            catch (Exception ex)
            {
                Message = "Error while updating the request: " + ex.Message;
            }

            return RedirectToPage("/ErrorPage");
        }

    }
}
