using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace lab8
{
    public class DataBase
    {
        /* --------------------------------- ReadCSV -------------------------------- */
        public (List<List<object>?>, List<string>) readCSV(string path, char delimiter)
        {
            List<List<object>?> data = new List<List<object>?>();
            List<string> header = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(path);

            // Create header
            header = lines[0].Split(delimiter).ToList();

            // Create data
            // Loop over lines
            foreach (string line in lines.Skip(1))
            {
                string[] values = line.Split(delimiter);
                List<object>? row = new List<object>();
                if (values.Length != header.Count)
                    throw new Exception("Invalid CSV format");

                // Loop over values
                foreach (string value in values)
                {
                    // If empty
                    if (string.IsNullOrWhiteSpace(value))
                        row.Add(null);

                    // Try to parse as double
                    else if (value.All(c => char.IsDigit(c) || c == '.'))
                        row.Add(Convert.ToDouble(double.Parse(value)));
   
                    // Try to parse as int
                    else if (value.All(char.IsDigit))
                        row.Add(int.Parse(value));
 
                    

                    // Try to parse as string
                    else
                        row.Add(value);
                }
                data.Add(row);
            }
            // Print
            System.Console.WriteLine("Header: " + string.Join(" ", header));
            foreach (List<object> row in data)
                System.Console.WriteLine(string.Join("| ", row));

            return (data, header);
        }



        /* ------------------------------- ColumnTypes ------------------------------ */
        public Dictionary<string, Tuple<Type, bool>> columnTypes(List<List<object>?> data, List<string> headers)
        {
            Dictionary<string, Tuple<Type, bool>> columnTypes = new Dictionary<string, Tuple<Type, bool>>();

            // Loop over columns
            foreach (string header in headers)
            {
                List<object> column = data.Select(row => row[headers.IndexOf(header)]).ToList();
                bool can_be_null = column.Contains(null);

                // Type of the first non-null value
                Type type = column.First(value => value != null).GetType();

                // Check if all values are of the same type
                if (column.All(value => value == null || value.GetType() == type))
                    columnTypes.Add(header, new Tuple<Type, bool>(type, can_be_null));
                else
                    System.Console.WriteLine("Column " + header + " has multiple types");

                // print
                System.Console.WriteLine("Column " + header + " is of type " + type + " and can be null: " + can_be_null);
            }
            return columnTypes;
        }

        /* ------------------------------- createTable ------------------------------ */
        public void createTable(Dictionary<string, Tuple<Type, bool>> columns, string tableName, SqliteConnection connection)
        {
            // Delete table if exists
            SqliteCommand delTableCmd = connection.CreateCommand();
            delTableCmd.CommandText = "DROP TABLE IF EXISTS " + tableName;
            delTableCmd.ExecuteNonQuery();

            // Create table
            SqliteCommand createTableCmd = connection.CreateCommand();
            string create_command = "CREATE TABLE " + tableName + " (";
            foreach (KeyValuePair<string, Tuple<Type, bool>> column in columns)
            {
                create_command += $"{column.Key} {GetSqliteType(column.Value.Item1)}";
                // If nullable
                if (!column.Value.Item2)
                    create_command += " NOT NULL";
                create_command += ", ";
            }
            create_command = create_command.TrimEnd(',') + ")";

            // Send command
            System.Console.WriteLine(create_command);
            createTableCmd.CommandText = create_command;
            createTableCmd.ExecuteNonQuery();

        }

        /* ------------------------------- insertData ------------------------------- */
        public void insertData(List<List<object>?> data, List<string> headers, string tableName, SqliteConnection connection)
        {
            // Insert data
            SqliteCommand insertCmd = connection.CreateCommand();
            string insert_command = "INSERT INTO " + tableName + " VALUES (";
            foreach (List<object>? row in data)
            {
                foreach (object? value in row)
                {
                    if (value == null)
                        insert_command += "NULL, ";
                    else if (value.GetType() == typeof(string))
                        insert_command += $"'{value}', ";
                    else
                        insert_command += $"{value}, ";
                }
                insert_command = insert_command.TrimEnd(',', ' ') + ")";
                insertCmd.CommandText = insert_command;
                insertCmd.ExecuteNonQuery();
                System.Console.WriteLine(insert_command);
            }
        }

        /* ------------------------------- printTable ------------------------------- */
        public void printTable(string tableName, SqliteConnection connection)
        {
            // Print table
            SqliteCommand printCmd = connection.CreateCommand();
            printCmd.CommandText = "SELECT * FROM " + tableName;
            SqliteDataReader reader = printCmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    System.Console.Write(reader.GetValue(i) + " ");
                System.Console.WriteLine();
            }
        }












        private string GetSqliteType(Type type)
        {
            // Map C# types to SQLite types
            if (type == typeof(int))
                return "INTEGER";
            else if (type == typeof(double))
                return "REAL";
            else if (type == typeof(string))
                return "TEXT";
            else if (type == typeof(bool))
                return "BOOLEAN";
            else
                throw new NotSupportedException($"Type {type.FullName} not supported by SQLite.");

        }


    }
}