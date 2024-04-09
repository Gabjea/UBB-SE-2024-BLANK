﻿using System;
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
    internal class PostSavedRepository
    {
        private PostSavedService service;

        private DatabaseConnection dbInstance;
        private SqlConnection conn;

        public PostSavedRepository()
        {
            service = new PostSavedService();
            dbInstance = DatabaseConnection.Instance;
            conn = dbInstance.GetConnection();
        }

        public bool addPostSavedtoDB(PostSaved postSaved)
        {
            string query = "INSERT INTO post_saves (save_id,post_id,user_id) Values (@save_id,@post_id,@user_id)";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@save_id", postSaved.save_id);
                command.Parameters.AddWithValue("@post_id", postSaved.post_id);
                command.Parameters.AddWithValue("@user_id", postSaved.user_id);
                

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

        public bool removePostSavedFromDB(PostSaved postSaved)
        {
            string query = "DELETE FROM post_saves WHERE save_id = @save_id";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                // Add parameters to the command to prevent SQL injection
                command.Parameters.AddWithValue("@save_id", postSaved.save_id);

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

       public List<PostSaved> getAll()
        {
            List<PostSaved> postSavedList = new List<PostSaved>();
            string query = "SELECT * FROM post_saves";

            conn.Open();

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostSaved postSaved = new PostSaved(
                                                       Guid.Parse(reader.GetString(0)),
                                                                                  Guid.Parse(reader.GetString(1)),
                                                                                                             Guid.Parse(reader.GetString(2))
                                                                                                                                    );
                        postSavedList.Add(postSaved);
                    }
                }
            }

            conn.Close();
            return postSavedList;
        }

    }
}
