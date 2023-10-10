using System.Text.Json.Serialization;

namespace CancherksWebApp.Model
{
    public class UserLogged
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("lastName2")]
        public string LastName2 { get; set; } 

        [JsonPropertyName("Rol")]
        public int Rol { get; set; }
    }
}
