using System.ComponentModel.DataAnnotations;

namespace CUSTOMSOFT.API.Models
{
    public class ApplicationModel
    {
        [Required(ErrorMessage = "El nombre de la aplicacion es Obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del cliente debe tener al menos 1 carácter.")]
        public string Nombre { get; set; }
        public string ApiKey { get; set; }
    }
}
