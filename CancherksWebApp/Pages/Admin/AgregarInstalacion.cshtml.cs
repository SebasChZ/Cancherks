using CancherksWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using CancherksWebApp.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace CancherksWebApp.Pages.Admin
{
    public class AgregarInstalacionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IWebHostEnvironment webHostEnvironment { get; }
        public string role { get; set; }

        public AgregarInstalacionModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public List<Sport> Sports { get; set; }
        public List<Day> Days { get; set; }

        [BindProperty]
        public Installation Installation { get; set; }

        [BindProperty]
        public int SelectedSportId { get; set; }

        public int radio { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }


        public string Message { get; set; }
        
        public void OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Days = _context.Day.ToList();
        }

        public async Task<IActionResult> OnPost(Installation installation, int radio)
        {
            try
            {
               
                Sport selectedSport = _context.Sport.Find(radio);

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

                if (Photo != null)
                {
                    string uniqueFileName = await ProcessUploadedFile();

                    if (uniqueFileName != null)
                    {
                        parameters[3].Value = uniqueFileName;
                    }
                    else
                    {
                        parameters[3].Value = uniqueFileName;
                    }                    
                }             

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spAddInstallationSchedule @name, @location, @description, @picture, @maxCantPeople,@timeSplitReservation,@idSport", parameters);

                Message = "Instalaci�n agregada con �xito!";
            }
            catch (Exception ex)
            {
                Message = "Error al agregar la instalaci�n: " + ex.Message;
            }

            return RedirectToPage("/Admin/AgregarInstalacion");
        }
        private async Task<string> ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(Photo.FileName); // Generate a unique file name
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", uniqueFileName); // Assuming you want to save it in an 'uploads' directory

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Photo.CopyToAsync(fileStream);
                }

                // Now filePath contains the complete route of the saved file on the server
                return uniqueFileName;
            }


            return null;
        }
    }

    
}
