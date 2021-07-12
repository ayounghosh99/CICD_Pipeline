using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class Database
    {
        public Database()
        {
            Privileges = new List<string>();
            Schemas = new List<Schema>();
        }

        public String Name { get; set; }
        public List<String> Privileges { get; set; }
        public List<Schema> Schemas { get; set; }
    }
}
