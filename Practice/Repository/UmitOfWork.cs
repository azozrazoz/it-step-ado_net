using Practice.UoW;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice.Repository
{
    public class UnitOfWork
    {
        private readonly SqlConnection _sqlConnection;
        private readonly UnitOfWorkContext _unitOfWorkContext;
        public TransactionUOW transactionRepository { get; private set; }
        public WalletUOW walletRepository { get; private set; }
        public ConvertCryptoUOW ConvertRepository { get; private set; }

        public UnitOfWork(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();

            _unitOfWorkContext = new UnitOfWorkContext(_sqlConnection);

            transactionRepository = new TransactionUOW(_unitOfWorkContext);
            walletRepository = new WalletUOW(_unitOfWorkContext);
            ConvertRepository = new ConvertCryptoUOW(_unitOfWorkContext);
        }
        public void SaveChanges()
        {
            //_unitOfWorkContext.SetExceptionOccuredDuringTransaction();
            _unitOfWorkContext.Commit();
        }
    }

    public class UnitOfWorkContext
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlTransaction _sqlTransaction;
        private bool _exceptionOccuredDuringTransactionScope;

        public UnitOfWorkContext(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = _sqlConnection.BeginTransaction();
            _exceptionOccuredDuringTransactionScope = false;
        }

        public void SetExceptionOccuredDuringTransaction()
        {
            _exceptionOccuredDuringTransactionScope = true;
        }

        public SqlCommand GetSqlCommandContainer()
        {
            var sqlCommandContainer = new SqlCommand()
            {
                Connection = _sqlConnection,
                Transaction = _sqlTransaction
            };
            return sqlCommandContainer;
        }

        public void Commit()
        {
            try
            {
                //надо проверить
                if (_exceptionOccuredDuringTransactionScope)
                    throw new Exception("Exception occured during transaction scope");

                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            finally
            {
                _sqlTransaction.Dispose();
            }
        }
    }
}
