using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class WalletRepository
    {

        /*private static bool GetWalletByIdForRestore(int ID)
        {
            var query = $"SELECT IsDeleted FROM [Wallet] WHERE ID = {ID}";

            var queryCommand = _unitOfWorkContext.GetSqlCommandContainer();
            queryCommand.CommandText = query;
            string result = queryCommand.ExecuteScalar().ToString();           
            return ConvertBoolean.Convert(result);
        }*/

        public static void GetAllWallets(string sqlConnection, ref List<Wallet> wallets)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = "SELECT * FROM [Wallet]";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            var cursor = queryCommand.ExecuteReader();
            while (cursor.Read())
            {
                wallets.Add(new Wallet(cursor.GetInt32(0), cursor.GetInt32(1), cursor.GetInt32(2), cursor.GetDouble(3), 
                    cursor.GetDateTime(4), cursor.GetDateTime(5), cursor.GetDateTime(6), cursor.GetBoolean(7)));
            }
            _sqlConnection.Close();
        }

        public static void AddWallet(string sqlConnection, int clientId)
        {
            Console.WriteLine("Enter wallet id: ");
            int walletId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter crypto id: ");
            int cryptoId = Convert.ToInt32(Console.ReadLine());

            Wallet wallet = new Wallet(walletId, clientId, cryptoId);

            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = "INSERT INTO [Wallet](" +
                "[ID], [ClientId], [CryptoId], [CryptoAmount], [CreateOn], [UpdateOn], [DeleteOn], [IsDeleted]) " +
                $"VALUES ({wallet.Id}, {wallet.ClientId}, {wallet.CryptoId}, {wallet.CryptoAmount}, " +
                $"'{wallet.CreateOn}', '{wallet.UpdateOn}', '{wallet.DeleteOn}', '{ConvertBoolean.Convert(wallet.IsDeleted)}')";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static void DeleteWallet(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"UPDATE Wallet SET DeleteOn = '{DateTime.Now}', IsDeleted = 1 WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            Console.WriteLine("(Wallet is deleted!)");
        }

        public static void DirectDeleteWallet(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"DELETE FROM [Wallet] WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static void UpdateWalletCryptoAmountPlus(string sqlConnection, int ID, double cryptoAmount)
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            var query = $"UPDATE [Wallet] SET CryptoAmount += {cryptoAmount}, UpdateOn = '{DateTime.Now}' WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, connection);
            queryCommand.ExecuteNonQuery();

            Console.WriteLine("Wallet updated!");
        }

        public static void UpdateWalletCryptoAmountMinus(string sqlConnection, int ID, double cryptoAmount)
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            var query = $"UPDATE [Wallet] SET CryptoAmount -= {cryptoAmount}, UpdateOn = '{DateTime.Now}' WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, connection);
            queryCommand.ExecuteNonQuery();

            Console.WriteLine("Wallet updated!");
        }

        public static void RestoreWallet(string sqlConnection, int ID)
        {

            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"UPDATE [Wallet] SET DeleteOn = CreateOn, IsDeleted = 0";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            Console.WriteLine("Wallet restored!");

        }
    }
}
