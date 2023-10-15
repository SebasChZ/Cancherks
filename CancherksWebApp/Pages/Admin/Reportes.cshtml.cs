using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using ClosedXML.Excel;
using System.Globalization;


namespace CancherksWebApp.Pages.Admin
{
    public class ReportesModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        public string role { get; set; }

        public ReportesModel(ApplicationDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public List<Installation> Installations { get; set; }
        public List<Request> Requests { get; set; }
        public List<Activity> Activities { get; set; }
        public List<State> States { get; set; }

        public List<UserAPIModel> PersonList { get; set; }
        public string RawJsonData { get; set; }

        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");

            Installations = _context.Installation.ToList();
            Requests = _context.Request.ToList();
            Activities = _context.Activity.ToList();
            States = _context.State.ToList();

            //for to iterate the list of requests and get the person data
            for (int i = 0; i < Requests.Count; i++)
            {
                UserAPIModel p = await LoadPersonData(Requests[i].EmailRequester);
                if (p != null)
                {
                    Requests[i].Person = p;

                }
            }
        }

        public async Task<UserAPIModel> LoadPersonData(string email)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://sistema-tec.somee.com/api/users/" + email);
            UserAPIModel p = new UserAPIModel();
            if (response.IsSuccessStatusCode)
            {

                // Attempt to deserialize the data
                try
                {
                    var data = await response.Content.ReadAsStringAsync();
                    p = JsonSerializer.Deserialize<UserAPIModel>(data);

                }
                catch (JsonException ex)
                {
                    // Log or output the deserialization error
                    RawJsonData = $"Error deserializing data: {ex.Message}";
                }


            }
            else
            {
                // Log or output the unsuccessful status code
                RawJsonData = $"Error: {response.StatusCode}";
            }
            return p;
        }

        public async Task<IActionResult> OnPostDownloadReport(string installationSelected, string startDate, string endDate)
        {
            Installations = _context.Installation.ToList();
            Requests = _context.Request.ToList();
            Activities = _context.Activity.ToList();
            States = _context.State.ToList();
            int installationId = Convert.ToInt32(installationSelected);
            
            //for to iterate the list of requests and get the person data
            for (int i = 0; i < Requests.Count; i++)
            {
                UserAPIModel p = await LoadPersonData(Requests[i].EmailRequester);
                if (p != null)
                {
                    Requests[i].Person = p;

                }
            }
            if (installationSelected != null)
            {
               
                if(installationId != 0)
                {
                    Requests = Requests.Where(r => r.IdInstallation == installationId).ToList();
                }
                
            }
            if (startDate != null && endDate != null)
            {
                DateTime start = Convert.ToDateTime(startDate);
                DateTime end = Convert.ToDateTime(endDate);
                Requests = Requests.Where(r => r.DateRequest >= start && r.DateRequest <= end).ToList();
            }
            else if (startDate != null && endDate == null)
            {
                DateTime start = Convert.ToDateTime(startDate);
                Requests = Requests.Where(r => r.DateRequest >= start).ToList();
            }
            else if(startDate == null && endDate != null)
            {
                DateTime end = Convert.ToDateTime(endDate);
                Requests = Requests.Where(r => r.DateRequest <= end).ToList();
            }
            List<String> filters = filterTittle(installationId, startDate, endDate);

            //get the date value 
            var excelData = GenerateExcelReport(Requests, Activities, States, Installations, filters);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Report.xlsx";
            return File(excelData, contentType, fileName);
        }

        public List<String> filterTittle(int installationId, string startDate, string endDate)
        {
            CultureInfo cultureInfo = new CultureInfo("es-ES");
            List<String> filters = new List<string>();
            //check if the installation is selected
            if (installationId != 0)
            {
                filters.Add("Instalación: " + Installations.FirstOrDefault(i => i.Id == installationId)?.Name);
            }
            else
            {
                filters.Add("Instalación: Todas");
            }

            //check if the start date is selected
            if (startDate != null)
            {
                DateTime start = Convert.ToDateTime(startDate);
                filters.Add("Fecha Inicio: " + start.ToString("dd 'de' MMMM 'del' yyyy", cultureInfo));
            }
            else
            {
                filters.Add("Fecha Inicio: Todas");
            }

            //check if the end date is selected
            if (endDate != null)
            {
                DateTime end = Convert.ToDateTime(endDate);
                filters.Add("Fecha Final: " + end.ToString("dd 'de' MMMM 'del' yyyy", cultureInfo));
            }
            else
            {
                filters.Add("Fecha Final: Todas");
            }   
            return filters;
        }

        public static byte[] GenerateExcelReport(List<Request> requests, List<Activity> activities, List<State> states, List<Installation> installations, List<String> filters)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Requests");

                // Merge the first row from column 1 to 10
                worksheet.Range("A1:J1").Merge();
                var titleCell = worksheet.Cell(1, 1);
                titleCell.Value = "Reporte de Solicitudes";
                titleCell.Style.Font.FontSize = 18;
                titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                titleCell.Style.Fill.BackgroundColor = XLColor.FromHtml("#C6EFCE");

                worksheet.Range("A2:J2").Merge();
                var installationCell = worksheet.Cell(2, 1);
                installationCell.Value = filters[0];
                installationCell.Style.Font.FontSize = 14;
                installationCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                //starDate cell
                worksheet.Range("A3:J3").Merge();
                var startDateCell = worksheet.Cell(3, 1);
                startDateCell.Value = filters[1];
                startDateCell.Style.Font.FontSize = 14;
                startDateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                //endDate cell
                worksheet.Range("A4:J4").Merge();
                var endDateCell = worksheet.Cell(4, 1);
                endDateCell.Value = filters[2];
                endDateCell.Style.Font.FontSize = 14;
                endDateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                // Add headers in row 5 with background color and bold style
                var headerStyle = worksheet.Range("A5:J5").Style;
                headerStyle.Font.Bold = true;
                headerStyle.Font.FontSize = 12;
                headerStyle.Fill.BackgroundColor = XLColor.FromHtml("#FFEB9C");

                worksheet.Cell(5, 1).Value = "Nombre";
                worksheet.Cell(5, 2).Value = "Apellido 1";
                worksheet.Cell(5, 3).Value = "Apellido 2";
                worksheet.Cell(5, 4).Value = "Correo";
                worksheet.Cell(5, 5).Value = "Fecha";
                worksheet.Cell(5, 6).Value = "Nombre Instalacion";
                worksheet.Cell(5, 7).Value = "Hora Inicio";
                worksheet.Cell(5, 8).Value = "Hora Final";
                worksheet.Cell(5, 9).Value = "Actividad";
                worksheet.Cell(5, 10).Value = "Estado";

                // Fill in the data starting from row 6
                int row = 6;
                foreach (var request in requests)
                {
                    worksheet.Cell(row, 1).Value = request.Person.PersonName;
                    worksheet.Cell(row, 2).Value = request.Person.FirstLastName;
                    worksheet.Cell(row, 3).Value = request.Person.SecondLastName;
                    worksheet.Cell(row, 4).Value = request.EmailRequester;
                    worksheet.Cell(row, 5).Value = request.DateRequest.ToShortDateString();
                    worksheet.Cell(row, 6).Value = installations.FirstOrDefault(i => i.Id == request.IdInstallation)?.Name;
                    worksheet.Cell(row, 7).Value = request.StartTime.ToString();
                    worksheet.Cell(row, 8).Value = request.EndTime.ToString();
                    worksheet.Cell(row, 9).Value = activities.FirstOrDefault(i => i.Id == request.IdActivity)?.Name;
                    worksheet.Cell(row, 10).Value = states.FirstOrDefault(i => i.Id == request.IdState)?.Name;

                    row++;
                }

                // Automatically adjust all cells to content
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }





        }


    }
}
