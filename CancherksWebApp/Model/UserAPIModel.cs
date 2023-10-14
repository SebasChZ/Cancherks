using System.Text.Json.Serialization;

namespace CancherksWebApp.Model
{
    public class UserAPIModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("personName")]
        public string PersonName { get; set; }

        [JsonPropertyName("firstLastName")]
        public string FirstLastName { get; set; }

        [JsonPropertyName("secondLastName")]
        public string SecondLastName { get; set; }

        [JsonPropertyName("debt")]
        public decimal Debt { get; set; }

        [JsonPropertyName("employee")]
        public Employee Employee { get; set; } // Assuming Employee can be a complex object

        [JsonPropertyName("student")]
        public Student Student { get; set; }

        [JsonPropertyName("departments")]
        public List<string> Departments { get; set; } // Assuming Department is a list of string

        [JsonPropertyName("schools")]
        public List<string> Schools { get; set; }     // Assuming School is a list of string

        [JsonPropertyName("applicationRoles")]
        public List<ApplicationRole> ApplicationRoles { get; set; }
    }
}
