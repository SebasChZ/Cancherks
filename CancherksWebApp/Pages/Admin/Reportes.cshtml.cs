using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;


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

        public Person Person { get; set; }

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
    }
}
