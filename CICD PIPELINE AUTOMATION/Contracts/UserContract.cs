using System;
using CICD_PIPELINE_AUTOMATION.Models;

namespace CICD_PIPELINE_AUTOMATION.Contracts
{
    public interface IUserContract
    {
        public string LoginUser(SUser suser);
    }
}
