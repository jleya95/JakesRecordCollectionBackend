using Microsoft.IdentityModel.Tokens;
using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RecordCollection.DAO
{
    public class SingleSqlDao : ISingleDao
    {
        private readonly string connectionString;

        public SingleSqlDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SingleEP GetSingle(string title, string artist, string serial, string releaseYear, string issueYear)
        {
            SingleEP singleToGet = new SingleEP();
            string sql = "SELECT * FROM singles WHERE title=@title AND artist=@artist AND serial_number=@serial " +
                "AND release_year=@releaseYear AND issue_year = @issueYear";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@artist", artist);
                    cmd.Parameters.AddWithValue("@serial", serial);
                    cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
                    cmd.Parameters.AddWithValue("@issueYear", issueYear);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        singleToGet.Id = Convert.ToInt32(reader["single_id"]);
                        singleToGet.File = Convert.ToString(reader["file_as"]);
                        singleToGet.Artist = Convert.ToString(reader["artist"]);
                        singleToGet.Title = Convert.ToString(reader["title"]);
                        singleToGet.ReleaseYear = Convert.ToInt32(reader["release_year"]);
                        singleToGet.Label = Convert.ToString(reader["record_label"]);
                        singleToGet.IssueYear = Convert.ToInt32(reader["issue_year"]);
                        singleToGet.SerialNumber = Convert.ToString(reader["serial_number"]);
                        singleToGet.Pressing = Convert.ToString(reader["pressing"]);
                        singleToGet.Color = Convert.ToString(reader["color"]);
                        singleToGet.Notes = Convert.ToString(reader["notes"]);
                    }
                    return singleToGet;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckSingleExistence(SingleEP single)
        {
            string sql = "SELECT single_id FROM singles " +
                "WHERE title = @title AND artist = @artist AND serial_number = @serial_number";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", single.Title);
                    cmd.Parameters.AddWithValue("@artist", single.Artist);
                    cmd.Parameters.AddWithValue("@serial_number", single.SerialNumber);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            return false;
        }

        public bool AddSingleToDb(SingleEP single)
        {
            int singleId = 0;

            string sql = "INSERT INTO singles (file_as, artist, title, release_year, record_label, issue_year, serial_number, pressing, color, notes) " +
                "OUTPUT INSERTED.single_id " +
                "VALUES (@file_as, @artist, @title, @release_year, @record_label, @issue_year, @serial_number, @pressing, @color, @notes);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@file_as", single.File);
                    cmd.Parameters.AddWithValue("@artist", single.Artist);
                    cmd.Parameters.AddWithValue("@title", single.Title);
                    cmd.Parameters.AddWithValue("@release_year", single.ReleaseYear);
                    cmd.Parameters.AddWithValue("@record_label", single.Label);
                    cmd.Parameters.AddWithValue("@issue_year", single.IssueYear);
                    cmd.Parameters.AddWithValue("@serial_number", single.SerialNumber);
                    if (!single.Pressing.IsNullOrEmpty())
                    {
                        cmd.Parameters.AddWithValue("@pressing", single.Pressing);
                    } else
                    {
                        cmd.Parameters.AddWithValue("@pressing", "");
                    }
                    if (!single.Color.IsNullOrEmpty())
                    {
                        cmd.Parameters.AddWithValue("@color", single.Color);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@color", "");
                    }
                    if (!single.Notes.IsNullOrEmpty())
                    {
                        cmd.Parameters.AddWithValue("@notes", single.Notes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@notes", "");
                    }

                    singleId = Convert.ToInt32(cmd.ExecuteScalar());
                    return true;
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<SingleEP> GetAllSingles()
        {
            List<SingleEP> singles = new List<SingleEP>();

            string sql = "SELECT * from singles ORDER BY file_as, release_year";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SingleEP singleToAdd = new SingleEP();

                        singleToAdd.Id = Convert.ToInt32(reader["single_id"]);
                        singleToAdd.File = Convert.ToString(reader["file_as"]);
                        singleToAdd.Artist = Convert.ToString(reader["artist"]);
                        singleToAdd.Title = Convert.ToString(reader["title"]);
                        singleToAdd.ReleaseYear = Convert.ToInt32(reader["release_year"]);
                        singleToAdd.Label = Convert.ToString(reader["record_label"]);
                        singleToAdd.IssueYear = Convert.ToInt32(reader["issue_year"]);
                        singleToAdd.SerialNumber = Convert.ToString(reader["serial_number"]);
                        singleToAdd.Pressing = Convert.ToString(reader["pressing"]);
                        singleToAdd.Color = Convert.ToString(reader["color"]);
                        singleToAdd.Notes = Convert.ToString(reader["notes"]);

                        singles.Add(singleToAdd);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return singles;
        }

    }


}

