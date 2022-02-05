using Practice.UoW;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class ConvertCryptoRepository
    {
        public static void GetAllConvertsCryptos(string sqlConnection, ref List<ConvertCrypto> converts)
        {
            SqlConnection connection = new SqlConnection(sqlConnection);
            connection.Open();
            var query = $"SELECT * FROM [Transaction]";

            var queryCommand = new SqlCommand(query, connection);
            var cursor = queryCommand.ExecuteReader();
            while (cursor.Read())
            {
                converts.Add(new ConvertCrypto(cursor.GetInt32(0), cursor.GetInt32(1), cursor.GetInt32(2), 
                    cursor.GetInt32(3), cursor.GetDouble(4),
                    cursor.GetDouble(5), cursor.GetInt32(6), 
                    cursor.GetInt32(7)));
            }
            connection.Close();
        }

        public static void AddConverts(string sqlConnection, ConvertCrypto convert)
        {
            double toSum = GetToSum(sqlConnection, convert.FromCryptoId, convert.ToCryptoId, convert.ToSum);

            SqlConnection _sqlConnection = new SqlConnection(sqlConnection);
            _sqlConnection.Open();
            
            var query = $"INSERT INTO [ConvertCrypto] ([ID], [ClientId], [FromCryptoId], [ToCryptoId], [FromSum], " +
                $"[ToSum], [FromWalletId], [ToWalletId]) VALUES ({convert.ID}, {convert.ClientId}, " +
                $"{convert.FromCryptoId}, {convert.ToCryptoId}, {ConvertBoolean.GetStrWithDot(convert.FromSum)}, " +
                $"{ConvertBoolean.GetStrWithDot(toSum)}, {convert.FromWalletId}, {convert.ToWalletId})";

            WalletUOW.UpdateWalletCryptoAmountMinus(convert.FromWalletId, convert.FromSum);
            WalletUOW.UpdateWalletCryptoAmountPlus(convert.ToWalletId, toSum);

            var queryCommand = new SqlCommand(query, _sqlConnection);
            queryCommand.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public static double GetPriceById(string connection, int CryptoId)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            var query = $"SELECT Price FROM [Crypto] where id = {CryptoId}";
            var command = new SqlCommand(query, sqlConnection);
            return (double)command.ExecuteScalar();
        }

        public static double GetToSum(string sqlConnection, int fCryptoId, int tCryptoId, double fSum)
        {
            double price1 = GetPriceById(sqlConnection, fCryptoId);
            double price2 = GetPriceById(sqlConnection, tCryptoId);
            return (price1 * fSum) / price2;
        }
    }
}
