using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CancherksWebApp.Model
{
    public class State
    {
        [Key]
        [Column("idState")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
