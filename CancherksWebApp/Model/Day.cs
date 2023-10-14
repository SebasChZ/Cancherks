using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class Day
    {
        [Key]
        [Column("idDay")]
        public int Id { get; set; }
        [Required]
        public string NameDay { get; set; }
    }
}
