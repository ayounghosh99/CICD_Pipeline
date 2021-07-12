using System;
using CICD_PIPELINE_AUTOMATION.Models;

namespace CICD_PIPELINE_AUTOMATION.Contracts
{
    public interface IDatabaseContract
    {
        public RoleInformation GetDatabase();
    }
}
