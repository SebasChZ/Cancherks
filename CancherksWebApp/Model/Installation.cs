using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CancherksWebApp.Model
{
    public class Installation
    {
        [Key]
        [Column("idInstallation")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "El nombre no debe ser un número.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La ubicación es requerida")]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "La ubicación no debe ser un número.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "La descripción no debe ser un número.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La foto es requerida")]
        public string Picture { get; set; } = "imagenPrueba.png";

        [Required(ErrorMessage = "La cantidad máxima de personas es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número entero.")]
        public int MaxCantPeople { get; set; }

        [Required(ErrorMessage = "El tiempo máximo por reservación es requerido")]
        [Range(0.0, 16.0, ErrorMessage = "El valor debe decimal y estar entre 0.0 y 16.0.")]
        public double TimeSplitReservation { get; set; }

        [Required(ErrorMessage = "El tiempo máximo por reservación es requerido")]
        public Boolean isPublic { get; set; }
    }
}
