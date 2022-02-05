using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.UoW
{
    public class TransactionUOW
    {
        private readonly UnitOfWorkContext _unitOfWorkContext;
        public TransactionUOW(UnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }
        public int AddTransaction(Transaction transaction)
        {
            var query = $"INSERT INTO [Transaction] " +
                "([ID], [CreateOn], [CryptoId], [Sum], [FromClientId], [ToClientId], [FromWalletId], [ToWalletId]) " +
                $"values ({transaction.ID}, '{DateTime.Now}', {transaction.CryptoId}, {transaction.Sum}, " +
                $"{transaction.FromClientId}, {transaction.ToClientId}, {transaction.FromWalletId}, {transaction.ToWalletId});";

            WalletUOW.UpdateWalletCryptoAmountMinus(transaction.FromWalletId, transaction.Sum);
            WalletUOW.UpdateWalletCryptoAmountPlus(transaction.ToWalletId, transaction.Sum);

            var queryCommand = _unitOfWorkContext.GetSqlCommandContainer();
            queryCommand.CommandText = query;
            return queryCommand.ExecuteNonQuery();
        }
    }
}
