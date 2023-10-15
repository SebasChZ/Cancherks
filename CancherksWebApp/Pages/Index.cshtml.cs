using Azure;
using CancherksWebApp.Data;
using CancherksWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CancherksWebApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;

        public UserAPIModel User { get; set; }


        public string RawJsonData { get; set; }

		[BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync(string email)
        {   
            string id = HttpContext.Session.GetString("email");

            if (string.IsNullOrEmpty(email))
            {
                //check for session
                if (string.IsNullOrEmpty(id))
                {
                    Email = "Default or Anonymous User";
                    Response.Redirect("/ErrorPage");
                }   
                // Now make the asynchronous call to the external API
                else if (HttpContext.Session.GetString("role") == "7415")
                {
                    Response.Redirect("/Solicitante/Reservacion");
                }
                else if (HttpContext.Session.GetString("role") == "2569")
                {
                    Response.Redirect("/Admin/GestionSolicitudes");
                }


            }
            else
            {
                Email = email;
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync("http://sistema-tec.somee.com/api/users/" + Email);

                if (response.IsSuccessStatusCode)
                {

                    // Attempt to deserialize the data
                    try
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        RawJsonData = data; // Use a logging framework or another method to inspect 'data'
                        User = JsonSerializer.Deserialize<UserAPIModel>(data);

                        HttpContext.Session.SetString("SessionKey", Guid.NewGuid().ToString());
                        HttpContext.Session.SetString("id", User.Id.ToString());
                        HttpContext.Session.SetString("email", User.Email.ToString());
                        HttpContext.Session.SetString("name", User.FirstLastName.ToString());
                        HttpContext.Session.SetString("lastName", User.FirstLastName.ToString());
                        HttpContext.Session.SetString("lastName2", User.SecondLastName.ToString());


                        for (int i = 0; i < User.ApplicationRoles.Count; i++)
                        {
                            if (User.ApplicationRoles[i].ApplicationId == 2) //this is the id of the application Cancherks in the database
                            {
                                if (User.ApplicationRoles[i].Id == 2)
                                {
                                    HttpContext.Session.SetString("role", "2569"); //admin
                                    break;
                                }
                                else if (User.ApplicationRoles[i].Id == 12) //Estoy waiting for the value of the role
                                {
                                    HttpContext.Session.SetString("role", "7415"); //Normal requester user
                                }
                            }
                        }


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

            // Now make the asynchronous call to the external API
            if (HttpContext.Session.GetString("role") == "7415")
            {
                Response.Redirect("/Solicitante/Reservacion");
            }
            else if (HttpContext.Session.GetString("role") == "2569")
            {
                Response.Redirect("/Admin/GestionSolicitudes");
            }
            else
            {
                Response.Redirect("/ErrorPage");
            }




        }
    }

}