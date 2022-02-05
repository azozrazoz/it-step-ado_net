using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.UoW
{
    public class ConvertCryptoUOW
    {
        private readonly UnitOfWorkContext _unitOfWorkContext;
        public ConvertCryptoUOW(UnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public int AddConvertation(ConvertCrypto convert)
        {            
            var query = $"INSERT INTO [ConvertCrypto] ([ID], [ClientId], [FromCryptoId], [ToCryptoId], [FromSum], " +
                $"[ToSum], [FromWalletId], [ToWalletId]) VALUES ({convert.ID}, {convert.ClientId}, " +
                $"{convert.FromCryptoId}, {convert.ToCryptoId}, {ConvertBoolean.GetStrWithDot(convert.FromSum)}, " +
                $"{ConvertBoolean.GetStrWithDot(convert.ToSum)}, {convert.FromWalletId}, {convert.ToWalletId})";

            WalletUOW.UpdateWalletCryptoAmountMinus(convert.FromWalletId, convert.FromSum);
            WalletUOW.UpdateWalletCryptoAmountPlus(convert.ToWalletId, convert.ToSum);

            var queryCommand = _unitOfWorkContext.GetSqlCommandContainer();
            queryCommand.CommandText = query;
            return queryCommand.ExecuteNonQuery();
        }
        public static double GetPriceById(int CryptoId, SqlConnection connection)
        {
            var query = $"SELECT Price FROM [Crypto] where id = {CryptoId}";
            var queryCommand = new SqlCommand(query, connection);
            return (double)queryCommand.ExecuteScalar();
        }

        public static double GetToSum(int fCryptoId, int tCryptoId, double fSum, SqlConnection connection)
        {
            double price1 = GetPriceById(fCryptoId, connection);
            double price2 = GetPriceById(tCryptoId, connection);
            return (price1 * fSum) / price2;
        }
    }
}
