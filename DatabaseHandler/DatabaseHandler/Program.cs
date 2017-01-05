using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseHandler
{
    class Program
    {
        private const string ConnetionString = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            InsertBook();
            DeleteBook();

            InsertBook();
            UpdateBook();
            SelectBook();
            Console.ReadLine();

            DeleteBook();
        }

        private static void InsertBook()
        {
            using (SqlConnection connection = new SqlConnection(ConnetionString))
            {
                var cmd = new SqlCommand("INSERT INTO [BookStore].[dbo].[Books] (BookId, Title, Author, Year) VALUES (@BookId, @Title, @Author, @Year)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@BookId", "8");
                cmd.Parameters.AddWithValue("@Title", "Star Wars");
                cmd.Parameters.AddWithValue("@Author", "James Dean");
                cmd.Parameters.AddWithValue("@Year", 234);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateBook()
        {
            using (SqlConnection connection = new SqlConnection(ConnetionString))
            {
                var cmd = new SqlCommand("UPDATE [BookStore].[dbo].[Books] SET ReaderName = @reader");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@reader", "Yoda");
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void DeleteBook()
        {
            using (SqlConnection connection = new SqlConnection(ConnetionString))
            {
                var cmd = new SqlCommand("DELETE FROM [BookStore].[dbo].[Books] WHERE BookId=8");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static void SelectBook()
        {
            using (SqlConnection connection = new SqlConnection(ConnetionString))
            {
                var cmd = new SqlCommand("SELECT Title FROM [BookStore].[dbo].[Books] WHERE ReaderName = @reader");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@reader", "Yoda");
                connection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr[0].ToString());
                    }
                }
            }
        }
    }
}
