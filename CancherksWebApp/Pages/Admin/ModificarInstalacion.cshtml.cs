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
    public class ModificarInstalacionModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IWebHostEnvironment webHostEnvironment { get; }
        public string role { get; set; }

        public ModificarInstalacionModel(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }


        public List<Sport> Sports { get; set; }
        public List<Day> Days { get; set; }

        [BindProperty]
        public Installation Installation { get; set; }
        public InstallationxSport InstallationxSport { get; set; }

        [BindProperty]
        public int SelectedSportId { get; set; }

        public int radio { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public ScheduleAvailability ScheduleAvailability { get; set; }
        [BindProperty]
        public string Message { get; set; }
        public List<ScheduleAvailability> ScheduleAvailabilities { get; set; }

        public void OnGet(int id)
        {
            role = HttpContext.Session.GetString("role");
            Sports = _context.Sport.ToList();
            Days = _context.Day.ToList();
            Installation = _context.Installation.Find(id);

            ScheduleAvailabilities = _context.ScheduleAvailability
                                     .Where(sa => sa.IdInstallation == id)
                                     .ToList();

            var installationSport = _context.InstallationxSport.FirstOrDefault(instSport => instSport.IdInstallation == id);
            if (installationSport != null)
            {
                SelectedSportId = installationSport.IdSport;
            }

            

        }
        public ScheduleAvailability GetScheduleForDay(int installationId, int dayId)
        {
            return _context.ScheduleAvailability.FirstOrDefault(sa => sa.IdInstallation == installationId && sa.IdDay == dayId);
        }


        public async Task<IActionResult> OnPostAddInstallation(Installation installation, int radio, int radioPub)
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
                    new SqlParameter("@idSport", selectedSport.Id),
                    new SqlParameter("@isPublic", radioPub),
                    new SqlParameter("@newId", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                if (Photo != null)
                {
                    string uniqueFileName = await ProcessUploadedFile();

                    parameters[3].Value = uniqueFileName ?? parameters[3].Value;
                }

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spAddInstallationSchedule @name, @location, @description, @picture, @maxCantPeople, @timeSplitReservation, @idSport, @isPublic, @newId OUTPUT", parameters);

                int newInstallationId = Convert.ToInt32(parameters[7].Value);
                TempData["NewInstallationId"] = newInstallationId;

                Message = "Instalaci�n agregada con �xito!";
            }
            catch (Exception ex)
            {
                Message = "Error al agregar: " + ex.Message;
            }
            return RedirectToPage("/Admin/AgregarInstalacion");
        }

        public async Task<IActionResult> OnPostAddSchedule(ScheduleAvailability scheduleAvailabilities)
        {
            try
            {
                int newInstallationId = (int)TempData["NewInstallationId"]; // Recuperamos el id de TempData

                var scheduleParameters = new SqlParameter[]
                {
                        new SqlParameter("@startTime", ScheduleAvailability.StartTime),
                        new SqlParameter("@endTime", ScheduleAvailability.EndTime),
                        new SqlParameter("@idDay", ScheduleAvailability.IdDay),
                        new SqlParameter("@idInstallation", newInstallationId) // Aqu� usamos el nuevo idInstallation
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spAddSchedule @startTime, @endTime, @idDay, @idInstallation", scheduleParameters);


                Message = "Horarios agregados con �xito!";
            }
            catch (Exception ex)
            {
                Message = "Error al agregar: " + ex.Message;
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


