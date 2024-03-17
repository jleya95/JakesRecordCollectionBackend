using Capstone.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Capstone.DAO
{
    public class RecordSqlDao : IRecordDao
    {
        private readonly string connectionString;
        public RecordSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool CheckRecordExistence(Record record)
        {
            string sql = "SELECT record_id FROM records " +
                "WHERE title = @title AND artist = @artist AND serial_number = @serial_number";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", record.Title);
                    cmd.Parameters.AddWithValue("@artist", record.Artist);
                    cmd.Parameters.AddWithValue("@serial_number", record.SerialNumber);
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

        public Record GetRecord(string title, string artist, string serial, string releaseYear, string issueYear)
        {
            Record recordToAdd = new Record();
            string sql = "SELECT * FROM records WHERE title=@title AND artist=@artist AND serial_number=@serial " +
                "AND release_year=@releaseYear AND issue_year = @issueYear";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@artist", artist);
                    if (!serial.IsNullOrEmpty())
                    {
                        cmd.Parameters.AddWithValue("@serial", serial);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@serial", "");
                    }
                    cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
                    cmd.Parameters.AddWithValue("@issueYear", issueYear);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        recordToAdd.Id = Convert.ToInt32(reader["record_id"]);
                        recordToAdd.File = Convert.ToString(reader["file_as"]);
                        recordToAdd.Artist = Convert.ToString(reader["artist"]);
                        recordToAdd.Title = Convert.ToString(reader["title"]);
                        recordToAdd.ReleaseYear = Convert.ToInt32(reader["release_year"]);
                        recordToAdd.Label = Convert.ToString(reader["record_label"]);
                        recordToAdd.IssueYear = Convert.ToInt32(reader["issue_year"]);
                        recordToAdd.SerialNumber = Convert.ToString(reader["serial_number"]);
                        recordToAdd.Pressing = Convert.ToString(reader["pressing"]);
                        recordToAdd.DiscNumber = Convert.ToInt32(reader["disc_number"]);
                        recordToAdd.Color = Convert.ToString(reader["color"]);
                        recordToAdd.Notes = Convert.ToString(reader["notes"]);
                        recordToAdd.NeedleInfo = Convert.ToString(reader["needle_info"]);
                    }
                    return recordToAdd;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddRecordToDb(Record record)
        {
            int recordId = 0;

            string sql = "INSERT INTO records (file_as, artist, title, release_year, record_label, issue_year, serial_number, pressing, disc_number, color, notes, needle_info)" +
                "OUTPUT INSERTED.record_id " +
                "VALUES (@file_as, @artist, @title, @release_year, @record_label, @issue_year, @serial_number, @pressing, @disc_number, @color, @notes, @needle_info);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@file_as", record.File);
                    cmd.Parameters.AddWithValue("@artist", record.Artist);
                    cmd.Parameters.AddWithValue("@title", record.Title);
                    cmd.Parameters.AddWithValue("@release_year", record.ReleaseYear);
                    cmd.Parameters.AddWithValue("@record_label", record.Label);
                    cmd.Parameters.AddWithValue("@issue_year", record.IssueYear);
                    cmd.Parameters.AddWithValue("@serial_number", record.SerialNumber);
                    cmd.Parameters.AddWithValue("@pressing", record.Pressing);
                    cmd.Parameters.AddWithValue("@disc_number", record.DiscNumber);
                    cmd.Parameters.AddWithValue("@color", record.Color);
                    cmd.Parameters.AddWithValue("@notes", record.Notes);
                    cmd.Parameters.AddWithValue("@needle_info", record.NeedleInfo);

                    recordId = Convert.ToInt32(cmd.ExecuteScalar());
                    return true;
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<Record> GetAllRecords()
        {
            List<Record> records = new List<Record>();

            string sql = "SELECT * from records ORDER BY file_as, release_year";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Record recordToAdd = new Record();

                        recordToAdd.Id = Convert.ToInt32(reader["record_id"]);
                        recordToAdd.File = Convert.ToString(reader["file_as"]);
                        recordToAdd.Artist = Convert.ToString(reader["artist"]);
                        recordToAdd.Title = Convert.ToString(reader["title"]);
                        recordToAdd.ReleaseYear = Convert.ToInt32(reader["release_year"]);
                        recordToAdd.Label = Convert.ToString(reader["record_label"]);
                        recordToAdd.IssueYear = Convert.ToInt32(reader["issue_year"]);
                        recordToAdd.SerialNumber = Convert.ToString(reader["serial_number"]);
                        recordToAdd.Pressing = Convert.ToString(reader["pressing"]);
                        recordToAdd.DiscNumber = Convert.ToInt32(reader["disc_number"]);
                        recordToAdd.Color = Convert.ToString(reader["color"]);
                        recordToAdd.Notes = Convert.ToString(reader["notes"]);
                        recordToAdd.NeedleInfo = Convert.ToString(reader["needle_info"]);

                        records.Add(recordToAdd);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            return records;
        }

    }
}