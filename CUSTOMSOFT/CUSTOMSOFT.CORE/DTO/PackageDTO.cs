using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.CORE.DTO
{
    public class PackageDTO
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerName { get; set; }
        public string DeliveryAddress { get; set; }
        public string? StateDescription { get; set; }
        public DateTime StateDate { get; set; }
        public decimal Weight { get; set; }
        public List<PackageStateDTO> States { get; set; }

    }
}
