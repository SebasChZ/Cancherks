using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CancherksWebApp.Pages.Admin
{
    public class ReportesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public string role { get; set; }

        public ReportesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Installation> Installations { get; set; }
        public List<Request> Requests { get; set; }
        public List<Activity> Activities { get; set; }
        public List<State> States { get; set; }

        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");

            Installations = _context.Installation.ToList();
            Requests = _context.Request.ToList();
            Activities = _context.Activity.ToList();
            States = _context.State.ToList();
        }
    }
}
