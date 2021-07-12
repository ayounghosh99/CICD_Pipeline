using System;
using System.Collections.Generic;
using CICD_PIPELINE_AUTOMATION.Contracts;
using CICD_PIPELINE_AUTOMATION.Models;
using Snowflake.Data.Client;

namespace CICD_PIPELINE_AUTOMATION.Services
{
    public class DatabaseService : IDatabaseContract
    {
        public readonly IConInfo conInfo;
        public DatabaseService(IConInfo conInfo)
        {
            this.conInfo = conInfo;
        }

        public RoleInformation GetDatabase()
        {
            RoleInformation roleInformation = new RoleInformation();
            string conStr = conInfo.getConStr();
            List<string> test = new List<string>();
            using (var conn = new SnowflakeDbConnection())
            {
                conn.ConnectionString = conStr;
                conn.Open();
                var cmd1 = conn.CreateCommand();
                cmd1.CommandText = "show databases;";
                var reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    roleInformation.databases.Add(new Database { Name = reader1.GetString(1) });
                }

                conn.Close();
            }

            return roleInformation;
        }
    }
}
