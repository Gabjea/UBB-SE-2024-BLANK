using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.services;
using Microsoft.Data.SqlClient;
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
            string query = "INSERT INTO post_reported (post_id, owner_user_id, description, commented_post_id, original_post_id, media_path, post_type, location_id, created_date) VALUES (@post_id, @owner_user_id, @description, @commented_post_id, @original_post_id , @media_path, @post_type, @location_id, @created_date)";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@owner_user_id", postReported.PostId);
                command.Parameters.AddWithValue("@post_id", postReported.ReportedPostId);
                command.Parameters.AddWithValue("@description", postReported.message);
                command.Parameters.AddWithValue("@commented_post_id", postReported.ReporterId);
     

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
            string query = "DELETE FROM post_reported WHERE post_id = @post_id";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@post_id", postReported.ReportedPostId);

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
            string query = "SELECT * FROM post_reported";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostReported postReported = new PostReported(
                                                       reader.GetGuid(0),
                                                                                  reader.GetGuid(1),
                                                                                                             reader.GetString(2),
                                                                                                                                        reader.GetGuid(3)
                                                                                                                                                               );
                        postReportedList.Add(postReported);
                    }
                }
            }
            conn.Close();
            return postReportedList;
        }

    }
}
