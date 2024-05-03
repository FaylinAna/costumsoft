using System.ComponentModel.DataAnnotations;

namespace CUSTOMSOFT.API.Models
{
    public class PackageModel
    {
        public string TrackingNumber { get; set; }
    }

    public class PackageAddModel
    {

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre del cliente debe tener al menos 1 carácter.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "La dirección de entrega es obligatoria.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La dirección de entrega debe tener al menos 1 carácter.")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "El peso debe ser mayor que cero.")]
        public decimal Weight { get; set; }

    }



}
