using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class RoleInformation
    {
        public RoleInformation()
        {
            this.databases = new List<Database>();
            this.warehouses = new List<Warehouse>();
        }

        public List<Database> databases { get; set; }
        public List<Warehouse> warehouses { get; set; }
    }
}
