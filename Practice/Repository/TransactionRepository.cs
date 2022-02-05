using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class TransactionRepository
    {       
        public static void GetAllTransaction(string sqlConnection, ref List<Transaction> transactions)
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            var query = $"SELECT * FROM [Transaction]";

            var queryCommand = new SqlCommand(query, connection);
            var cursor = queryCommand.ExecuteReader();
            while (cursor.Read())
            {
                transactions.Add(new Transaction(cursor.GetInt32(0), cursor.GetDateTime(1), cursor.GetInt32(2), cursor.GetInt32(3), cursor.GetInt32(4),
                    cursor.GetDouble(5), cursor.GetInt32(6), cursor.GetInt32(7)));
            }
        }

        public static void AddTransaction(string _sqlConnection, Transaction transaction)
        {
            SqlConnection connection = new SqlConnection(_sqlConnection);
            connection.Open();
            var query = $"INSERT INTO [Transaction] " +
                "([ID], [CreateOn], [CryptoId], [Sum], [FromClientId], [ToClientId], [FromWalletId], [ToWalletId]) " +
                $"values ({transaction.ID}, '{DateTime.Now}', {transaction.CryptoId}, {transaction.Sum}, " +
                $"{transaction.FromClientId}, {transaction.ToClientId}, {transaction.FromWalletId}, {transaction.ToWalletId});";

            var queryCommand = new SqlCommand(query, connection);
            queryCommand.ExecuteNonQuery();
        }

        public static void DeleteTransaction(string sqlConnection, int ID)
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            var query = $"delete from [Transaction] where ID = {ID}";

            var queryCommand = new SqlCommand(query, connection);
            queryCommand.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Transaction is deleted!");
        }
    }
}
