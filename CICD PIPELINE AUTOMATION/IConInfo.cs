using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_PIPELINE_AUTOMATION
{
    public interface IConInfo
    {
        public void setCon(string str);

        public string getConStr();
    }
}
