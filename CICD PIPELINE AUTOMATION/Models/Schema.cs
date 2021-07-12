using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION.Models
{
    public class Schema
    {
        public Schema()
        {
            Privileges = new List<string>();
            Tables = new List<string>();
            TablePrivileges = new List<string>();
            Views = new List<string>();
            ViewPrivileges = new List<string>();
            Streams = new List<string>();
            StreamPrivileges = new List<string>();
            Tasks = new List<string>();
            TaskPrivileges = new List<string>();
            Procedures = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Privileges { get; set; }
        public List<string> Tables { get; set; }
        public List<string> TablePrivileges { get; set; }
        public List<string> Views { get; set; }
        public List<string> ViewPrivileges { get; set; }
        public List<string> Streams { get; set; }
        public List<string> StreamPrivileges { get; set; }
        public List<string> Tasks { get; set; }
        public List<string> TaskPrivileges { get; set; }
        public List<string> Procedures { get; set; }
        public bool PrevOnFutureTasks { get; set; }
        public bool PrevOnFutureTables { get; set; }
        public bool PrevOnFutureStreams { get; set; }
        public bool PrevOnFutureViews { get; set; }

    }
}
