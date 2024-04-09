﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.services;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using client.models;

namespace client.repositories
{
    internal class PostReportedRepository
    {
        private DatabaseConnection dbInstance;
        private SqlConnection conn;
        private PostReportedService service;

        public PostReportedRepository(PostReportedService _service)
        {
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
            service = _service;
        }

        public bool addReportedPostToDB(PostReported postReported)
        {
            string query = "INSERT INTO post_reports (report_id,reason,description,post_id,reporter_id) Values (@report_id,@reason,@description,@post_id,@reporter_id)";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@report_id", postReported.report_id);
                command.Parameters.AddWithValue("@reason", postReported.reason);
                command.Parameters.AddWithValue("@description", postReported.description);
                command.Parameters.AddWithValue("@post_id", postReported.post_id);
                command.Parameters.AddWithValue("@reporter_id", postReported.reporter_id);
     

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public bool removeReportedPostFromDB(PostReported postReported)
        {
            string query = "DELETE FROM post_reports WHERE report_id = @report_id";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@report_id", postReported.report_id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    return false;
                }
            }
            conn.Close();
            return true;
        }

        public List<PostReported> getAll()
        {
            List<PostReported> postReportedList = new List<PostReported>();
            string query = "SELECT * FROM post_reports";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostReported postReported = new PostReported(Guid.Parse(reader.GetString(0)),reader.GetString(1),reader.GetString(2), Guid.Parse(reader.GetString(3)), Guid.Parse(reader.GetString(4)));
                                                                                                                                                 
                        postReportedList.Add(postReported);
                    }
                }
            }
            conn.Close();
            return postReportedList;
        }

    }
}
