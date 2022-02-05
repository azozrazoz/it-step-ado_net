using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Practice.Repository;
using Practice.UoW;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Server=ASUS-DOSHAN;" +
                "Database=MARKET;" +
                "Trusted_Connection=True;";

            #region commands

            //Commands for manipulation data clients :>


            //List<ClientEntity> c = new List<ClientEntity>();
            //ClientRepository.GetAllClients(connectionString, ref c);

            //ClientRepository.AddClient(connectionString);
            //ClientRepository.DeleteClient(connectionString, 1);
            //ClientRepository.RestoreClient(connectionString, 1);
            //ClientRepository.UpdateClient(connectionString, 1);

            //-------------------------------------------------

            //Commands for manipulation data wallets :)

            //List<Wallet> wallets = new List<Wallet>();
            //WalletRepository.GetAllWallets(connectionString, ref wallets);

            //WalletRepository.AddWallet(connectionString, 2);
            //WalletRepository.DirectDeleteWallet(connectionString, 2345);
            //WalletRepository.DeleteWallet(connectionString, 4321);
            //WalletRepository.RestoreWallet(connectionString, 4321);
            //WalletRepository.UpdateWalletCryptoAmountPlus(connectionString, 2345, 200);
            //WalletRepository.UpdateWalletCryptoAmountMinus(5678, 100);

            //-------------------------------------------------

            //Commands for manipulation data cryptos :^

            //List<Crypto> cryptos = new List<Crypto>();
            //CryptoRepository.GetAllCryptos(connectionString, ref cryptos);

            //CryptoRepository.AddCrypto(connectionString);
            //CryptoRepository.DirectDeleteCrypto(connectionString, 3);
            //CryptoRepository.DeleteCrypto(connectionString, 2);
            //CryptoRepository.RestoreCrypto(connectionString, 2);
            //CryptoRepository.UpdateCryptoPrice(connectionString, 1, 34782.34);

            //-------------------------------------------------

            //Commands for manipulation data transactions :|

            //List<Transaction> t = new List<Transaction>();
            //TransactionRepository.GetAllTransaction(connectionString, ref t);
            //TransactionRepository.AddTransaction(connectionString, < new transaction > );
            //TransactionRepository.DeleteTransaction(connectionString, < ID >);

            //-------------------------------------------------
            //ConvertCrypto convert = new ConvertCrypto(1, 1, 1, 2, 100, 1234, 4321);
            //ConvertCryptoRepository.AddConverts(connectionString, convert);


            /*foreach (var item in t)
            {
                Console.WriteLine(item.ID);
            }*/
            #endregion

            #region transactionUoW
            //TRANSACTIONS
            /*
                        Console.WriteLine("Enter id transaction: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id from fromClientId: ");
                        int fromClientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id from toClientId: ");
                        int toClientId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id from cryptoId: ");
                        int cryptoId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter from sum: ");
                        double sum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id from fromWalletId: ");
                        int fromWalletId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter id from toWalletId: ");
                        int toWalletId = Convert.ToInt32(Console.ReadLine());

                        Transaction transaction = new Transaction(id, fromClientId, toClientId, cryptoId, sum, fromWalletId, toWalletId);

                        var unitOfWork = new UnitOfWork(connectionString);

                        var transactionUnit = unitOfWork.transactionRepository;

                        int integred = transactionUnit.AddTransaction(transaction);
                        Console.WriteLine(integred);
                        unitOfWork.SaveChanges();*/
            #endregion

            #region convertation
            //CONVERTATIONS

            /*Console.WriteLine("Enter id convertation: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter id from ClientId: ");
            int clientId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter id from fromCryptoId: ");
            int fromCryptoId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter id from toCryptoId: ");
            int toCryptoId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter from sum: ");
            double sum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter id from fromWalletId: ");
            int fromWalletId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter id from toWalletId: ");
            int toWalletId = Convert.ToInt32(Console.ReadLine());

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            double toSum = ConvertCryptoUOW.GetToSum(fromCryptoId, toCryptoId, sum, connection);

            connection.Close();
            ConvertCrypto crypto = new ConvertCrypto(id, clientId, fromCryptoId, toCryptoId, sum, toSum, fromWalletId, toWalletId);

            var unitOfWork = new UnitOfWork(connectionString);

            var convertRep = unitOfWork.ConvertRepository;

            convertRep.AddConvertation(crypto);

            unitOfWork.SaveChanges();*/
            #endregion
        }
    }
}
