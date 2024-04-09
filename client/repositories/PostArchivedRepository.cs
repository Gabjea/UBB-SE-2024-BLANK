using client.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.services;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace client.repositories
{
    internal class PostArchivedRepository
    {
        private PostArchivedService service;

        private DatabaseConnection dbInstance;
        private SqlConnection conn;

        public PostArchivedRepository(PostArchivedService _service)
        {
            service = _service;
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
        }

        public bool addPostArchivedToDB(PostArchived postArchived)
        {
            string query = "INSERT INTO post_archive (archive_id,post_id) Values (@archive_id,@post_id)";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@archive_id", postArchived.archive_id);
                command.Parameters.AddWithValue("@post_id", postArchived.post_id);
                
          

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

        public bool removePostArchivedFromDB(PostArchived postArchived)
        {
            string query = "DELETE FROM post_archive WHERE archive_id = @archive_id";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@archive_id", postArchived.archive_id);

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

        public List<PostArchived> getAll()
        {
            List<PostArchived> postArchivedList = new List<PostArchived>();

            string query = "SELECT * FROM post_archive";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostArchived postArchived = new PostArchived(Guid.Parse(reader.GetString(0)), Guid.Parse(reader.GetString(1)));
                        postArchivedList.Add(postArchived);
                    }
                }
            }
            conn.Close();
            return postArchivedList;
        }
    }
}
