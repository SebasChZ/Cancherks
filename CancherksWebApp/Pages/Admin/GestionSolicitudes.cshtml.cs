
using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

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
        public List<Request> Requests { get; set; } 


        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList(); // Filtrar por idState == 1

            Page();

        }

        public async Task<IActionResult> OnPostAsync(string idSolicitud, string idSolicitudReject)
        {
            int? idRequest = null;

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

            // Refresh the data
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList();

            return RedirectToPage("/Admin/GestionSolicitudes");
        }


    }
}
