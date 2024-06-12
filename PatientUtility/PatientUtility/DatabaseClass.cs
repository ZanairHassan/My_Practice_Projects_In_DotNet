using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientUtility
{
    internal class DatabaseClass
    {
        public void InsertPatientData(string filePath)
{
    try
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.Trim().Contains("[Patient]"))
            {
                // Trim the line to remove any leading or trailing whitespace
               // string trimmedLine = line.Trim();

                // Remove the "[Patient]" keyword from the trimmed line
               // string patientData = trimmedLine.Substring("[Patient]".Length);

                // Splitting data based on '|'
                string[] data = line.Split('|');

                // Insert data into the database
                InsertIntoDatabase(data);
            }
        }
    }
    catch (Exception ex)
    {
        Logger.LogText($"An error occurred while reading data from file and inserting into database: {ex.Message}");
    }
}


        public void InsertIntoDatabase(string[] data)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SqlConnect"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertOrUpdatePatientDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@PatientId", data[1]);
                        command.Parameters.AddWithValue("@Prefix", data[2]);
                        command.Parameters.AddWithValue("@FirstName", data[3]);
                        command.Parameters.AddWithValue("@MiddleName", data[4]);
                        command.Parameters.AddWithValue("@LastName", data[5]);
                        command.Parameters.AddWithValue("@Suffix", data[6]);
                        command.Parameters.AddWithValue("@SSN", data[7]);
                        command.Parameters.AddWithValue("@DateOfBirth", data[8]);
                        command.Parameters.AddWithValue("@DeceasedTime", data[9]);
                        command.Parameters.AddWithValue("@Gender", data[10]);
                        command.Parameters.AddWithValue("@MaritalStatus", data[11]);
                        command.Parameters.AddWithValue("@Race", data[12]);
                        command.Parameters.AddWithValue("@Ethnicity", data[13]);
                        command.Parameters.AddWithValue("@ReligiousAffiliation", data[14]);
                        command.Parameters.AddWithValue("@Street", data[15]);
                        command.Parameters.AddWithValue("@City", data[16]);
                        command.Parameters.AddWithValue("@State", data[17]);
                        command.Parameters.AddWithValue("@PostalCode", data[18]);
                        command.Parameters.AddWithValue("@Country", data[19]);
                        command.Parameters.AddWithValue("@HomePhone", data[20]);
                        command.Parameters.AddWithValue("@CellPhone", data[21]);
                        command.Parameters.AddWithValue("@WorkPhone", data[22]);
                        command.Parameters.AddWithValue("@Email", data[23]);
                        command.Parameters.AddWithValue("@HICNumber", data[24]);
                        command.Parameters.AddWithValue("@InsuranceId", data[25]);
                        command.Parameters.AddWithValue("@PreferredLanguage", data[26]);
                        command.Parameters.AddWithValue("@SecondaryLanguage", data[27]);

                        command.ExecuteNonQuery();
                        Logger.LogText("The Data has successfully saved in the table");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.LogText($"An error occurred while inserting data into database: {ex.Message}");
            }
        }

    }
}
