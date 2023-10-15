using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text.Json;

namespace CancherksWebApp.Areas.Admin.Pages
{
    public class GestionSolcitudesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public string role { get; set; }
        public string Message { get; set; }
        public GestionSolcitudesModel(ApplicationDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public List<Installation> Installations { get; set; }
        public List<Request> Requests { get; set; }

        public Person Person { get; set; }

        public List<UserAPIModel> PersonList { get; set; }
        public string RawJsonData { get; set; }

        public async Task OnGet()
        {
            role = HttpContext.Session.GetString("role");
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList(); // Filtrar por idState == 1
            PersonList = new List<UserAPIModel>();
            //for to iterate the list of requests and get the person data
            for (int i = 0; i < Requests.Count; i++)
            {
                UserAPIModel p = await LoadPersonData(Requests[i].EmailRequester);
                if (p != null)
                {
                    Requests[i].Person = p;
                    PersonList.Add(p);
                }
            }

            Page();

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

        public async Task<IActionResult> OnPostAsync(string idSolicitud, string idSolicitudReject)
        {
            int? idRequest = null;

            if (!string.IsNullOrEmpty(idSolicitud))
            {
                idRequest = Convert.ToInt32(idSolicitud);
            }
            else if (!string.IsNullOrEmpty(idSolicitudReject))
            {
                idRequest = Convert.ToInt32(idSolicitudReject);
            }

            if (idRequest.HasValue)
            {
                var requestToUpdate = await _context.Request.FindAsync(idRequest.Value);

                if (requestToUpdate == null)
                {
                    Message = "Request not found";
                    return RedirectToPage("/Admin/GestionSolicitudes");
                }

                if (!string.IsNullOrEmpty(idSolicitud))
                {
                    requestToUpdate.IdState = 2;
                    Message = "Request state has been updated";
                }
                else
                {
                    requestToUpdate.IdState = 3;
                    Message = "Request has been rejected";
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Message = "Error while updating the request: " + ex.Message;
                }
            }
            else
            {
                Message = "No valid request ID provided.";
            }

            // Refresh the data
            Installations = _context.Installation.ToList();
            Requests = _context.Request.Where(r => r.IdState == 1).ToList();

            return RedirectToPage("/Admin/GestionSolicitudes");
        }

    }
}
