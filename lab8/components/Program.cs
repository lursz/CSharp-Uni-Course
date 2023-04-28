using Microsoft.Data.Sqlite;
using lab8;
class Program
{
    private static void Main(string[] args)
    {
        // snap install sqlitebrowser to host database
        DataBase db = new DataBase();
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "database/db.db";

        using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        // Read CSV file
        var (data, header) = db.readCSV("input/input1.csv", ',');

        // Get column types
        var columns = db.columnTypes(data, header);

        // Create table
        db.createTable(columns, "test", connection);


        // Insert data
        db.insertData(data, header, "test", connection);


        // Print table
        db.printTable("test", connection);
        

        connection.Close();

    }
}