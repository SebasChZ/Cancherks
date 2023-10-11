
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
        public List<Request> Requests { get; set; } // A�ade esta l�nea


        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList(); // Filtrar por idState == 1

            Page();

        }

        public async Task<IActionResult> OnPostAsync(string idSolicitud)
        {
            
            var idRequest = Convert.ToInt32(idSolicitud);   
            var modifyRequest = await _context.Request.FindAsync(idRequest);

            if (modifyRequest == null)
            {
                Message = "Request not found";
                Response.Redirect("/Admin/GestionSolicitudes");
            }

            modifyRequest.IdState = 2;

            try
            {
                await _context.SaveChangesAsync();
                Message = "Request state has been updated";
            }
            catch (Exception ex)
            {
                Message = "Error while updating the request: " + ex.Message;
            }

            // Inicializa las listas después de actualizar
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList(); // Filtrar por idState == 1

            // Redirige a la misma página
            return RedirectToPage("/Admin/GestionSolicitudes");
        }




    }
}
