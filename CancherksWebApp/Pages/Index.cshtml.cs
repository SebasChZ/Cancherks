using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CancherksWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Installation> Installations { get; set; }
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _clientFactory;

        public ApplicationRole ApplicationRole { get; set; }


        public UserLogged userLoggeed { get; set; }

        public string RawJsonData { get; set; }
        public string ErrorMessage { get; set; }

        public string SessionValue { get; set; }

        public IndexModel(ApplicationDbContext db, IHttpClientFactory clientFactory)
        {
            _db = db;
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {

            // Creating first UserLogged instance with provided and invented data
            UserLogged user1 = new UserLogged
            {
                Id = 2021052792,
                Email = "chcalerks@estudiantec.cr",
                Name = "Maynor",
                LastName = "Martínez",
                LastName2 = "Hernández",
                Rol = 7415 // Invented data
            };

            // Creating second UserLogged instance with provided and invented data
            UserLogged user2 = new UserLogged
            {
                Id = 202105271,
                Email = "fmMurillo@estudiantec.cr",
                Name = "Fernanda",
                LastName = "Murillo",
                LastName2 = "Mena",
                Rol = 2569  // Invented data
            };

            userLoggeed = user1; // Copying user1 to user3


            // You can perform synchronous operations here as well
            Installations = _db.Installation;

            // Set session value
            HttpContext.Session.SetString("SessionKey", Guid.NewGuid().ToString());
            HttpContext.Session.SetString("id", userLoggeed.Id.ToString());
            HttpContext.Session.SetString("email", userLoggeed.Email.ToString());
            HttpContext.Session.SetString("lastName", userLoggeed.LastName.ToString());
            HttpContext.Session.SetString("lastName2", userLoggeed.LastName2.ToString());
            HttpContext.Session.SetString("role", userLoggeed.Rol.ToString());




            // Now make the asynchronous call to the external API
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://sistema-tec.somee.com/api/applicationroles/10");

            if (response.IsSuccessStatusCode)
            {
                // Log or output the raw JSON response
                var data = await response.Content.ReadAsStringAsync();
                RawJsonData = data; // Use a logging framework or another method to inspect 'data'

                // Attempt to deserialize the data
                try
                {
                    ApplicationRole = JsonSerializer.Deserialize<ApplicationRole>(data);
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
        }
    }

}