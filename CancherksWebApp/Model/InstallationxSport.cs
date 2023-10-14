using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class InstallationxSport
    {
        [Key]
        [Column("idInstallationxSport")]
        public int Id { get; set; }

        [Required]
        public int IdInstallation { get; set; }

        [Required]
        public int IdSport { get; set; }
    }
}
