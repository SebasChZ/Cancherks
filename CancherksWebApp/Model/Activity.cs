using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class Activity
    {

        [Key]
        [Column("idActivity")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
