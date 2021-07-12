using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class Warehouse
    {
        public Warehouse()
        {
            this.Privileges = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Privileges { get; set; }

    }
}
