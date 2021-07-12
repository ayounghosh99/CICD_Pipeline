using System;
using CICD_PIPELINE_AUTOMATION.Models;

namespace CICD_PIPELINE_AUTOMATION.Contracts
{
    public interface IItemContract
    {
        public Database GetItems(Objects objects);

        public void SendItems(Objects data);
    }
}
