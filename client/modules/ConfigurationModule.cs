using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using client.models;

namespace client.modules
{
    internal class ConfigurationModule
    {

        private List<string> allowedFileExtensions;
        private int maxFileSize;
        private string GOOGLE_PLACES_API_KEY;

        private string DB_IP= Environment.GetEnvironmentVariable("DB_IP");
        private string DB_USER=Environment.GetEnvironmentVariable("DB_USER");
        private string DB_PASS= Environment.GetEnvironmentVariable("DB_PASS");
        private string DB_SCHEMA=Environment.GetEnvironmentVariable("DB_SCHEMA");


        public List<string> getAllowedFileExtensions()
        {
            return this.allowedFileExtensions;
        }

        public int getMaxFileSize()
        {
            return this.maxFileSize;
        }

        public string getGOOGLE_PLACES_API_KEY()
        {
            return this.GOOGLE_PLACES_API_KEY;
        }


        public ConfigurationModule()
        {
            DatabaseConnection dbInstance;
            SqlConnection conn;
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
            try
            {
                conn.Open();
                allowedFileExtensions = FetchConfigurationValue(conn, "allowedFileExtensions").Split(',').ToList();
                maxFileSize = int.Parse(FetchConfigurationValue(conn, "maxFileSize"));
                GOOGLE_PLACES_API_KEY = FetchConfigurationValue(conn, "GOOGLE_PLACES_API_KEY");
            }
            catch (Exception ex)
            {
                //log error
            }
        }

        private string FetchConfigurationValue(SqlConnection conn, string settingName)
        {
            string query = $"SELECT TOP 1 ValueColumn FROM configuration WHERE KeyColumn = @SettingName";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    throw new Exception($"Setting '{settingName}' not found.");
                }
            }
        }

    }
}
