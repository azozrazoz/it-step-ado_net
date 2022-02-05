using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.UoW
{
    public class WalletUOW
    {
        private static UnitOfWorkContext _unitOfWorkContext;
        public WalletUOW(UnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }
        public static void UpdateWalletCryptoAmountMinus(int ID, double cryptoAmount)
        {

            var query = $"UPDATE [Wallet] SET CryptoAmount -= {ConvertBoolean.GetStrWithDot(cryptoAmount)}, UpdateOn = '{DateTime.Now}' WHERE ID = {ID}";

            var queryCommand = _unitOfWorkContext.GetSqlCommandContainer();
            queryCommand.CommandText = query;
            queryCommand.ExecuteNonQuery();
        }

        public static void UpdateWalletCryptoAmountPlus(int ID, double cryptoAmount)
        {
            var query = $"UPDATE [Wallet] SET CryptoAmount += {ConvertBoolean.GetStrWithDot(cryptoAmount)}, UpdateOn = '{DateTime.Now}' WHERE ID = {ID}";

            var queryCommand = _unitOfWorkContext.GetSqlCommandContainer();
            queryCommand.CommandText = query;
            queryCommand.ExecuteNonQuery();

            Console.WriteLine("Wallet updated!");
        }
    }
}
