using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class ScheduleAvailability
    {
        [Key]
        [Column("idScheduleAvailability")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column(TypeName = "Time")]
        public TimeSpan EndTime { get; set; }
        [Required]
        public int IdDay { get; set; }
        [Required]
        public int IdInstallation { get; set; }
    }
}
