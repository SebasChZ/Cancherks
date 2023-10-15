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

        public List<InstallationxSport> InstallationxSports { get; set; }
                                                           

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
        

        public async Task<IActionResult> OnPostModifyInstallation(Installation installation, int radio, int radioPub)
        {
            try
            {
                Sport selectedSport = _context.Sport.Find(radio);
                var installationxSportEntry = _context.InstallationxSport.FirstOrDefault(ixs => ixs.IdInstallation == Installation.Id);
                if (installationxSportEntry != null)
                {
                    int idInstallationxSport = installationxSportEntry.Id;
                }

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@idInstallation", installationxSportEntry.IdInstallation),
                    new SqlParameter("@name", installation.Name),
                    new SqlParameter("@description", installation.Description),
                    new SqlParameter("@maxCantPeople", installation.MaxCantPeople),
                    new SqlParameter("@timeSplitReservation", installation.TimeSplitReservation),
                    new SqlParameter("@isPublic", radioPub),
                    new SqlParameter("@idInstallationxSport", installationxSportEntry.Id),
                    new SqlParameter("@idSport", selectedSport.Id)
                };

               
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spUpdateInstallation @idInstallation, @name, @description, @maxCantPeople, @timeSplitReservation, @isPublic,@idInstallationxSport, @idSport", parameters);

               
                Message = "Instalación modificada con éxito!";
            }
            catch (Exception ex)
            {
                Message = "Error al modificar: " + ex.Message;
            }
            return RedirectToPage("/Admin/ModificarInstalacion");
        }

        public async Task<IActionResult> OnPostModifySchedule(ScheduleAvailability scheduleAvailabilities)
        {
            
            try
            {
                var installationSchedule = _context.ScheduleAvailability.FirstOrDefault(ixs => ixs.IdInstallation == Installation.Id);
                if (installationSchedule != null)
                {
                    int idSchedule = installationSchedule.Id;
                }

                var scheduleParameters = new SqlParameter[]
                {
                        new SqlParameter("@idInstallation", installationSchedule.IdInstallation),
                        new SqlParameter("@idScheduleAvailability", installationSchedule.Id),
                        new SqlParameter("@startTime", ScheduleAvailability.StartTime),
                        new SqlParameter("@endTime", ScheduleAvailability.EndTime),
                        new SqlParameter("@idDay", ScheduleAvailability.IdDay)
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spUpdateScheduleAvailability @idInstallation, @idScheduleAvailability, @startTime, @endTime, @idDay", scheduleParameters);


                Message = "Horarios modificados con éxito!";
            }
            catch (Exception ex)
            {
                Message = "Error al modificar: " + ex.Message;
            }
            return RedirectToPage("/Admin/ModificarInstalacion");
        }

        
    }
}


