using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class Objects
    {
        public Objects()
        {
            schemas = new List<Schema>();
            schemas.Add(new Schema { Name = "RAW" });
            schemas.Add(new Schema { Name = "STAGE" });
            schemas.Add(new Schema { Name = "DWH" });
        }

        public string SourceDb { get; set; }
        public string TargetDb { get; set; }

        //public List<String> Tables { get; set; }
        //public List<string> Views { get; set; }
        //public List<string> Procedures { get; set; }
        //public List<string> Streams { get; set; }
        //public List<string> Tasks { get; set; }

        public List<Schema> schemas { get; set; } 
    }
}
