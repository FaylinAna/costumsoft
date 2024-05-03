using CUSTOMSOFT.INFRAESTRUCTURE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.INFRAESTRUCTURE.Models
{
    public class PackageState
    {
        public int IdPackageStates { get; set; }
        public int PackageId { get; set; }
        public string State { get; set; }
        public DateTime StateDatePackages { get; set; }
    }
}


