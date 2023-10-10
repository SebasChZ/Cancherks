using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CancherksWebApp.Model
{
    public class Installation
    {
        [Key]
        [Column("idInstallation")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public int MaxCantPeople { get; set; }
        [Required]
        public double TimeSplitReservation { get; set; }
    }

   

}
