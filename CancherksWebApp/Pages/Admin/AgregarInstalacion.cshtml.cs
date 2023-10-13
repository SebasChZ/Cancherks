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

        public List<Sport> Sports { get; set; }
        public List<Day> Days { get; set; }

        [BindProperty]
        public Installation Installation { get; set; }

        [BindProperty]
        public List<int> SelectedSports { get; set; } = new List<int>();
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Days = _context.Day.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _context.Installation.AddAsync(Installation);
                await _context.SaveChangesAsync();

                // Guarda las asociaciones con los deportes seleccionados
                foreach (var sportId in SelectedSports)
                {
                    var installationxSport = new InstallationxSport
                    {
                        InstallationId = Installation.Id,
                        SportId = sportId
                    };
                    _context.InstallationxSports.Add(installationxSport);
                }

                await _context.SaveChangesAsync();

            }
            // Aquí puedes redireccionar a otra página si lo deseas o mostrar un mensaje
            return RedirectToPage("/Admin/AgregarInstalacion");
        }
    }
}
