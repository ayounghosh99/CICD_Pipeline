using System;
using CICD_PIPELINE_AUTOMATION.Contracts;
using CICD_PIPELINE_AUTOMATION.Models;
using Snowflake.Data.Client;

namespace CICD_PIPELINE_AUTOMATION.Services
{
    public class UserService : IUserContract
    {
        private readonly IConInfo conInfo;
        public UserService(IConInfo conInfo)
        {
            this.conInfo = conInfo;
        }

        public string LoginUser(SUser suser)
        {
            if (suser.LoginUrl != null && suser.User != null && suser.Password != null)
            {
                //string conStr = @"scheme=https;ACCOUNT=ija33209;HOST=ija33209.snowflakecomputing.com;port=443; " +
                //    "ROLE=ACCOUNTADMIN;WAREHOUSE=COMPUTE_WH; USER=Shailaja; PASSWORD=Passw0rd;DB=DEMO_DB;SCHEMA=PUBLIC";
                var loginurl = suser.LoginUrl.Split("//");
                var host = loginurl[1];
                var account = host.Split('.')[0];
                var role = suser.Role;
                var warehouse = suser.Warehouse;
                var username = suser.User;
                var password = suser.Password;
                string conStr = "scheme=https;ACCOUNT=" + account + ";HOST=" + host + ";port=443;ROLE=" + role + ";WAREHOUSE=" + warehouse +
                                                   "; USER=" + username + "; PASSWORD=" + password + ";";
                using (var conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = conStr;
                    conn.Open();
                    conInfo.setCon(conStr);
                    conn.Close();
                }
                return (conStr);
            }
            return null;
        }
    }
}
