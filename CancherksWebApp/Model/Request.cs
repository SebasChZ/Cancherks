using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class Request
    {
        [Key]
        [Column("idRequest")]
        public int Id { get; set; }
        [Required]
        public string EmailRequester { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateRequest { get; set; }
        [NotMapped]
        public int IdInstallation { get; set; }
        [Required]
        public int IdState { get; set; }
        [Required]
        public int IdActivity { get; set; }
        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan EndTime { get; set; }
    }
}
