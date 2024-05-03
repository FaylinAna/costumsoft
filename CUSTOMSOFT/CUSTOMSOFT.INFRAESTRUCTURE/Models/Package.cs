using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Models
{
    public class Package
    {
        public int IdPackage { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime StateDatePackages { get; set; }
        public decimal Weight { get; set; }
        public List<PackageState> States { get; set; }
    }
}
