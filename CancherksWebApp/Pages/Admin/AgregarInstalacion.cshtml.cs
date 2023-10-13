using CancherksWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using CancherksWebApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        public int SelectedSportId { get; set; }


        public string Message { get; set; }
        
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Days = _context.Day.ToList();
        }

        public async Task<IActionResult> OnPost(Installation installation, Sport sport)
        {
            try
            {
                Sport selectedSport = _context.Sport.Find(SelectedSportId);

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", installation.Name),
                    new SqlParameter("@location", installation.Location),
                    new SqlParameter("@description", installation.Description),
                    new SqlParameter("@picture", installation.Picture),
                    new SqlParameter("@maxCantPeople", installation.MaxCantPeople),
                    new SqlParameter("@timeSplitReservation", installation.TimeSplitReservation),
                    new SqlParameter("@idSport", selectedSport.Id)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spAddInstallationSchedule @name, @location, @description, @picture, @maxCantPeople,@timeSplitReservation,@idSport", parameters);

                Message = "Instalación agregada con éxito!";
            }
            catch (Exception ex)
            {
                Message = "Error al agregar la instalación: " + ex.Message;
            }

            return RedirectToPage("/Admin/AgregarInstalacion");
        }

    }
}
