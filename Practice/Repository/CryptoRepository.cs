using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class CryptoRepository
    {
        public static bool CheckCryptoById(string sqlConnection, int ID)
        {
            try
            {
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();

                var query = $"SELECT ID FROM [Crypto] WHERE ID = {ID}";

                var queryCommand = new SqlCommand(query, _sqlConnection);
                var result = queryCommand.ExecuteScalar();
                return (int)result > 0;
            }
            catch (Exception) { Console.WriteLine("Enter correct ID!"); return false; }
        }

        private static bool GetCryptoByIdForRestore(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();

            var query = $"SELECT IsDeleted FROM [Crypto] WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            string result = queryCommand.ExecuteScalar().ToString();
            return ConvertBoolean.Convert(result);
        }

        public static void GetAllCryptos(string sqlConnection, ref List<Crypto> cryptos)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = "SELECT * FROM [Crypto]";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            var cursor = queryCommand.ExecuteReader();
            while (cursor.Read())
            {
                cryptos.Add(new Crypto(cursor.GetInt32(0), cursor.GetDouble(1), 
                    cursor.GetDateTime(2), cursor.GetDateTime(3), cursor.GetDateTime(4), cursor.GetInt32(5), cursor.GetBoolean(6), cursor.GetString(7)));
            }
            _sqlConnection.Close();
        }

        public static void AddCrypto(string sqlConnection)
        {
            Console.WriteLine("Enter crypto id: ");
            int cryptoId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter crypto name: ");
            string cryptoName = Console.ReadLine();
            Console.WriteLine("Enter crypto price: ");
            double cryptoPrice = Convert.ToDouble(Console.ReadLine());

            Crypto crypto = new Crypto(cryptoId, cryptoPrice, cryptoName);

            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = "INSERT INTO [Crypto](" +
                "[ID], [Price], [CreateOn], [UpdateOn], [DeleteOn], [IsDeleted], [VersionRow], [_Name]) " +
                $"VALUES ({crypto.ID}, {crypto.Price}, " +
                $"'{crypto.CreateOn}', '{crypto.UpdateOn}', '{crypto.DeleteOn}', '{ConvertBoolean.Convert(crypto.IsDeleted)}', " +
                $"{crypto.VersionRow}, '{crypto.Name}')";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static void DeleteCrypto(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"UPDATE Crypto SET DeleteOn = '{DateTime.Now}', IsDeleted = 1 WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            Console.WriteLine("(Crypto is deleted!)");
        }

        public static void DirectDeleteCrypto(string sqlConnection, int ID)
        {
            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            var query = $"DELETE FROM [Crypto] WHERE ID = {ID}";

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static void UpdateCryptoPrice(string sqlConnection, int ID, double price)
        {
            if (!GetCryptoByIdForRestore(sqlConnection, ID))
            {
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();

                var query = $@"UPDATE [Crypto] SET Price = CAST({ConvertBoolean.GetStrWithDot(price)} AS FLOAT) WHERE ID = {ID}";
                //, UpdateOn = '{DateTime.Now}'
                var queryCommand = new SqlCommand(query, _sqlConnection);
                queryCommand.ExecuteNonQuery();
                _sqlConnection.Close();

                Console.WriteLine("Crypto updated!");
            }
            else
            {
                Console.WriteLine("Crypto is deleted or does not exist.");
            }
        }

        public static void RestoreCrypto(string sqlConnection, int ID)
        {
            if (GetCryptoByIdForRestore(sqlConnection, ID))
            {
                SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
                _sqlConnection.Open();
                var query = $"UPDATE [Crypto] SET DeleteOn = CreateOn, IsDeleted = 0";

                var queryCommand = new SqlCommand(query, _sqlConnection);
                queryCommand.ExecuteNonQuery();
                _sqlConnection.Close();
                Console.WriteLine("Crypto restored!");
            }
            else
            {
                Console.WriteLine("The crypto has not been deleted or he has been restored!");
            }
        }
    }
}
