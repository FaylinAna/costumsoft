using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMSOFT.CORE.DTO
{
    public class PackageStateDTO
    {
        public int Id { get; set; }
        public int Package_Id { get; set; }
        public string State { get; set; }
        public DateTime StateDatePackages { get; set; }
    }
}
