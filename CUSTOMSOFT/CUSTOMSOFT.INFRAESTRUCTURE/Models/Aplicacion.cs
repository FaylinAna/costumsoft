using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Models
{
    public class Aplicacion
    {
        public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public string ApiKey { get; set; }
        public string ClientSecretKey { get; set; }
        public string PrivateKey { get; set; }
        public bool? Activo { get; set; }
    }

    public class AplicacionVM
    {
        [Column("idaplicacion")]
        public int IdAplicacion { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apikey")]
        public string ApiKey { get; set; }

        [Column("clientsecretkey")]
        public string ClientSecretKey { get; set; }

        [Column("privatekey")]
        public string PrivateKey { get; set; }

        [Column("activo")]
        public bool? Activo { get; set; }
    }
}
