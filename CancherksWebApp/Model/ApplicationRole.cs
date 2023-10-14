using System.Text.Json.Serialization;

namespace CancherksWebApp.Model
{
    public class ApplicationRole
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("applicationId")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("applicationRoleName")]
        public string ApplicationRoleName { get; set; }

        [JsonPropertyName("applicationName")]
        public string ApplicationName { get; set; }

        [JsonPropertyName("parentId")]
        public int? ParentId { get; set; }

        [JsonPropertyName("inverseparent")]
        public List<object> InverseParent { get; set; } // Adjust the type as needed

        [JsonPropertyName("application")]
        public object Application { get; set; } // Adjust the type as needed

        [JsonPropertyName("parent")]
        public object Parent { get; set; } // Adjust the type as needed

        [JsonPropertyName("emails")]
        public List<string> Emails { get; set; }
    }

}
