using System.Text.Json.Serialization;

namespace CancherksWebApp.Model
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("isProfessor")]
        public bool IsProfessor { get; set; }

        [JsonPropertyName("emailNavigation")]
        public object EmailNavigation { get; set; } // Since
    }
}
