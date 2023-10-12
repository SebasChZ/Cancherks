using CancherksWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using CancherksWebApp.Model;

namespace CancherksWebApp.Pages.Admin
{
    public class AgregarInstalacionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public string role { get; set; }

        public AgregarInstalacionModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Activity> Activities { get; set; }
        public List<Day> Days { get; set; }

        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Activities = _context.Activity.ToList();
            Days = _context.Day.ToList();
        }
    }
}
