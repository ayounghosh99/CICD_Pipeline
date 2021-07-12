using System;
using System.Collections.Generic;
using System.Net;
using CICD_PIPELINE_AUTOMATION.Contracts;
using CICD_PIPELINE_AUTOMATION.Models;
using Snowflake.Data.Client;
using System.IO;

namespace CICD_PIPELINE_AUTOMATION.Services
{
    public class ItemService : IItemContract
    {
        private readonly IConInfo conInfo;
        public ItemService(IConInfo conInfo)
        {
            this.conInfo = conInfo;
        }

        public Database GetItems(Objects objects)
        {
            Database database = new Database();
            string conStr = conInfo.getConStr();
            using (var conn = new SnowflakeDbConnection())
            {
                conn.ConnectionString = conStr;
                conn.Open();
                List<string> SCHEMAS = new List<string>() { "DWH", "RAW", "STAGE" };
                //var cmd2 = conn.CreateCommand();
                //cmd2.CommandText = "show schemas in database " + objects.SourceDb + ";";
                //var reader2 = cmd2.ExecuteReader();
                foreach (var sch in SCHEMAS)
                {
                    var schema = new Schema { Name = sch };
                    //Tables
                    var tablecmd = conn.CreateCommand();
                    tablecmd.CommandText = "show tables in schema " + objects.SourceDb + "." + sch + ";";
                    var tablereader = tablecmd.ExecuteReader();
                    while (tablereader.Read())
                    {
                        schema.Tables.Add(tablereader.GetString(1));
                    }
                    //Streams
                    var streamcmd = conn.CreateCommand();
                    streamcmd.CommandText = "show streams in schema " + objects.SourceDb + "." + sch + ";";
                    var streamreader = streamcmd.ExecuteReader();
                    while (streamreader.Read())
                    {
                        schema.Streams.Add(streamreader.GetString(1));
                    }
                    //Procedures
                    var procedurecmd = conn.CreateCommand();
                    procedurecmd.CommandText = "show procedures in schema " + objects.SourceDb + "." + sch + ";";
                    var procedurereader = procedurecmd.ExecuteReader();
                    while (procedurereader.Read())
                    {
                        schema.Procedures.Add(procedurereader.GetString(1));
                    }
                    //Tasks
                    var taskcmd = conn.CreateCommand();
                    taskcmd.CommandText = "show tasks in schema " + objects.SourceDb + "." + sch + ";";
                    var taskreader = taskcmd.ExecuteReader();
                    while (taskreader.Read())
                    {
                        schema.Tasks.Add(taskreader.GetString(1));
                    }
                    //Views
                    var viewcmd = conn.CreateCommand();
                    viewcmd.CommandText = "show views in schema " + objects.SourceDb + "." + sch + ";";
                    var viewreader = viewcmd.ExecuteReader();
                    while (viewreader.Read())
                    {
                        schema.Views.Add(viewreader.GetString(1));
                    }
                    database.Schemas.Add(schema);
                }
                conn.Close();
            }

            return database;
        }

        public void SendItems(Objects data)
        {
            using (var conn = new SnowflakeDbConnection())
            {
                int id = 1;
                conn.ConnectionString = conInfo.getConStr();
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "Truncate table \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\"";
                cmd.ExecuteNonQuery();
                //var query = "";
                for (int i = 0; i < 3; i++)
                {
                    if (data.schemas[i] != null)
                    {
                        foreach (var table in data.schemas[i].Tables)
                        {
                            cmd.CommandText = "insert into \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\" values(" + id + ",'" + table + "','TABLE','" + data.schemas[i].Name +
                                "','" + data.SourceDb + "','" + data.TargetDb + "'); ";
                            cmd.ExecuteNonQuery();
                        }
                        foreach (var str in data.schemas[i].Streams)
                        {
                            cmd.CommandText = "insert into \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\" values(" + id + ",'" + str + "','STREAM','" + data.schemas[i].Name +
                                "','" + data.SourceDb + "','" + data.TargetDb + "'); ";
                            cmd.ExecuteNonQuery();
                        }
                        foreach (var task in data.schemas[i].Tasks)
                        {
                            cmd.CommandText = "insert into \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\" values(" + id + ",'" + task + "','TASK','" + data.schemas[i].Name +
                                "','" + data.SourceDb + "','" + data.TargetDb + "'); ";
                            cmd.ExecuteNonQuery();
                        }
                        foreach (var procedure in data.schemas[i].Procedures)
                        {
                            cmd.CommandText = "insert into \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\" values(" + id + ",'" + procedure + "','PROCEDURE','" + data.schemas[i].Name +
                                "','" + data.SourceDb + "','" + data.TargetDb + "'); ";
                            cmd.ExecuteNonQuery();
                        }
                        foreach (var view in data.schemas[i].Views)
                        {
                            cmd.CommandText = "insert into \"DEV_DB\".\"RAW\".\"SCHEMA_OBJECT_LIST\" values(" + id + ",'" + view + "','VIEW','" + data.schemas[i].Name +
                                "','" + data.SourceDb + "','" + data.TargetDb + "'); ";
                            cmd.ExecuteNonQuery();
                        }

                    }

                }

                // https://elastic.snaplogic.com:443/api/1/rest/slsched/feed/GuardianDev/DWBI/DW_DEV/CI_CD_PARENT%20Task
                //Authorization: Bearer N6Ia4jYgvgKmrc1ngh1bYLJEM4RbNjjU
                //https://elastic.snaplogic.com:443/api/1/rest/slsched/feed/GuardianDev/DWBI/DW_DEV/CI_CD_CHILD%20Task
                //Authorization: Bearer T4s9Uue6nuKv1rrr3JWNzeAx66FLBsu3


                HttpWebRequest request = WebRequest.Create("https://elastic.snaplogic.com:443/api/1/rest/slsched/feed/GuardianDev/DWBI/DW_DEV/CI_CD_PARENT%20Task") as HttpWebRequest;
                //optional
                WebHeaderCollection webHeaderCollection = request.Headers;
                webHeaderCollection.Add("Authorization: Bearer N6Ia4jYgvgKmrc1ngh1bYLJEM4RbNjjU");
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();

                conn.Close();
            }
        }
    }
}
