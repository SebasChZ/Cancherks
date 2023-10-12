using Azure.Core;
using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions;

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

        public JsonResult OnGetLoadDataInfo(int idInstallation, int idDay)
        {
            var data = _context.InstallationScheduleViewModels.FromSqlRaw("EXEC spGetInstallationAviability  @idInstallation={0} , @idDay={0}", idInstallation, idDay).ToList();
            return new JsonResult(data);
        }

        public void OnPostDownload(string idInstallation, string idSport)
        {
            //This is not being called <---------------------------------
            ViewData["Message"] = "You clicked Cancel!";
            RedirectToPage("/Admin/GestionSolicitudes");
            //InstallationSportViewModels = _context.InstallationSportViewModels.FromSqlRaw("EXEC spGetInstallationsbySport @idSport={0}", idSport).ToList();
        }
        public void OnPostSave()
        {
            ViewData["Message"] = "You clicked Save!";
        }

        public void OnPostCancel()
        {
            ViewData["Message"] = "You clicked Cancel!";
        }

        public async Task<IActionResult> asdfasdf(string idSolicitud, string idSolicitudReject)
        {
            int? idRequest = null;

            int installationId = Convert.ToInt32(idSolicitud);
            int sportId = Convert.ToInt32(idSolicitudReject);

            if (!string.IsNullOrEmpty(idSolicitud))
            {
                idRequest = Convert.ToInt32(idSolicitud);
            }
            else if (!string.IsNullOrEmpty(idSolicitudReject))
            {
                idRequest = Convert.ToInt32(idSolicitudReject);
            }

            if (idRequest.HasValue)
            {
                var requestToUpdate = await _context.Request.FindAsync(idRequest.Value);

                if (requestToUpdate == null)
                {
                    Message = "Request not found";
                    return RedirectToPage("/Admin/GestionSolicitudes");
                }

                if (!string.IsNullOrEmpty(idSolicitud))
                {
                    requestToUpdate.IdState = 2;
                    Message = "Request state has been updated";
                }
                else
                {
                    requestToUpdate.IdState = 3;
                    Message = "Request has been rejected";
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Message = "Error while updating the request: " + ex.Message;
                }
            }
            else
            {
                Message = "No valid request ID provided.";
            }


            return RedirectToPage("/Admin/GestionSolicitudes");
        }

    }
}
