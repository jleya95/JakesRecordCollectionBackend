using System;
using System.Data.SqlClient;

namespace RecordCollection.DAO
{
    public class DataForScriptSqlDao : IDataForScript
    {
        private readonly string connectionString;
        public DataForScriptSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public string GetRecordsValuesForSqlScript()
        {
            string sqlValuesString = "INSERT INTO records (file_as, artist, title, release_year, record_label, issue_year, " +
                "serial_number, pressing, disc_number, color, notes, needle_info) VALUES ";
            string sql = "SELECT * from records ORDER BY file_as, release_year";
            string[] split;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string fileAs = Convert.ToString(reader["file_as"]);
                        if (fileAs.Contains("'"))
                        {
                            split = fileAs.Split("'");
                            fileAs = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    fileAs += $"{split[i]}''";
                                }
                                else
                                {
                                    fileAs += split[i];
                                }
                            }
                        }
                        string artist = Convert.ToString(reader["artist"]);
                        if (artist.Contains("'"))
                        {
                            split = artist.Split("'");
                            artist = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    artist += $"{split[i]}''";
                                }
                                else
                                {
                                    artist += split[i];
                                }
                            }
                        }
                        string title = Convert.ToString(reader["title"]);
                        if (title.Contains("'"))
                        {
                            split = title.Split("'");
                            title = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    title += $"{split[i]}''";
                                }
                                else
                                {
                                    title += split[i];
                                }
                            }
                        }
                        int releaseYear = Convert.ToInt32(reader["release_year"]);
                        string recordLabel = Convert.ToString(reader["record_label"]);
                        if (recordLabel.Contains("'"))
                        {
                            split = recordLabel.Split("'");
                            recordLabel = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    recordLabel += $"{split[i]}''";
                                }
                                else { recordLabel += split[i]; };
                            }
                        }
                        int issueYear = Convert.ToInt32(reader["issue_year"]);
                        string serialNumber = Convert.ToString(reader["serial_number"]);
                        string pressing = Convert.ToString(reader["pressing"]);
                        if (pressing.Contains("'"))
                        {
                            split = pressing.Split("'");
                            pressing = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    pressing += $"{split[i]}''";
                                }
                                else
                                {
                                    pressing += split[i];
                                }
                            }
                        }
                        int discNumber = Convert.ToInt32(reader["disc_number"]);
                        string color = Convert.ToString(reader["color"]);
                        string notes = Convert.ToString(reader["notes"]);
                        if (notes.Contains("'"))
                        {
                            split = notes.Split("'");
                            notes = "";
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (i != (split.Length - 1))
                                {
                                    notes += $"{split[i]}''";
                                }
                                else { notes += split[i]; }
                            }
                        }
                        string needleInfo = Convert.ToString(reader["needle_info"]);

                        sqlValuesString += $"('{fileAs}', '{artist}', '{title}', {releaseYear}, " +
                                $"'{recordLabel}', {issueYear}, '{serialNumber}', '{pressing}', " +
                                $"{discNumber}, '{color}', '{notes}', '{needleInfo}'), \r\n";
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

            return sqlValuesString;
        }
    }
}
