using System;
using System.Data;
using Microsoft.Data.SQLClient;


public class Service
{
    public static string connectionString = "data source=LAPTOP-1U316NQH;initial catalog=AirportSoftware;trusted_connection=true";


    // Method to SELECT data
    public static DataTable GetAllRows(string tableName)
    {
        DataTable table = new DataTable();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = $"SELECT * FROM {tableName}";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table); // Load data into the DataTable
        }

        return table; // Return the DataTable
    }

    // Method to INSERT data
    public static int InsertRow(string tableName, string column1, string column2, string value1, string value2)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = $"INSERT INTO {tableName} ({column1}, {column2}) VALUES (@Value1, @Value2)";
            SqlCommand command = new SqlCommand(query, connection);

            // Add parameters to prevent SQL injection
            command.Parameters.AddWithValue("@Value1", value1);
            command.Parameters.AddWithValue("@Value2", value2);

            connection.Open();
            return command.ExecuteNonQuery(); // Returns the number of rows affected
        }
    }

    // Method to UPDATE data
    public static int UpdateRow(string tableName, string columnToUpdate, string newValue, string conditionColumn, string conditionValue)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = $"UPDATE {tableName} SET {columnToUpdate} = @NewValue WHERE {conditionColumn} = @ConditionValue";
            SqlCommand command = new SqlCommand(query, connection);

            // Add parameters
            command.Parameters.AddWithValue("@NewValue", newValue);
            command.Parameters.AddWithValue("@ConditionValue", conditionValue);

            connection.Open();
            return command.ExecuteNonQuery(); // Returns the number of rows affected
        }
    }

    // Method to DELETE data
    public static int DeleteRow(string tableName, string conditionColumn, string conditionValue)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = $"DELETE FROM {tableName} WHERE {conditionColumn} = @ConditionValue";
            SqlCommand command = new SqlCommand(query, connection);

            // Add parameter
            command.Parameters.AddWithValue("@ConditionValue", conditionValue);

            connection.Open();
            return command.ExecuteNonQuery(); // Returns the number of rows affected
        }
    }
}
}