using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CancherksWebApp.Areas.Solicitante.Pages
{
    public class MisReservacionesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }
        public MisReservacionesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }
        public List<Request> Requests { get; set; }
        public List<State> States { get; set; }

        public string flag { get; set; }

        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            var email = HttpContext.Session.GetString("email");
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.EmailRequester == email).ToList(); // Filtrar por idState == 1
            States = _context.State.ToList();

            if (Requests.Count == 0)
            {
                flag = HttpContext.Session.GetString("email"); ;
            }
            else
            {
                flag = "no ta vacio";
            }
            Page();
        }
    }
}
