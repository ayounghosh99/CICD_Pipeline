using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class ConInfo:IConInfo
    {
        public string connectionString { get; set; }

        public void setCon(string str)
        {
            connectionString = str;
        }
        public string getConStr()
        {
            return connectionString;
        }
    }
}
