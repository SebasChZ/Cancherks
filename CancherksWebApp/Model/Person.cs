using System.Text.Json.Serialization;

namespace CancherksWebApp.Model
{
    public class Person
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("personPassword")]
        public string PersonPassword { get; set; }

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
        public object Employee { get; set; }

        [JsonPropertyName("student")]
        public object Student { get; set; }

        [JsonPropertyName("applicationRoles")]
        public List<ApplicationRole> ApplicationRoles { get; set; }

        [JsonPropertyName("departments")]
        public List<object> Departments { get; set; }

        [JsonPropertyName("schools")]
        public List<object> Schools { get; set; }
    }
}
