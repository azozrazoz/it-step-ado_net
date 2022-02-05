using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Practice
{
    public class Transaction
    {
        public int ID { get; set; }
        public DateTime CreateOn { get; set; }
        public int FromClientId { get; set; }
        public int ToClientId { get; set; }
        public int CryptoId { get; set; }
        public double Sum { get; set; }
        public int FromWalletId { get; set; }
        public int ToWalletId { get; set; }

        //constructor for get
        public Transaction(int id, DateTime createon, int fromClient, int toClietn, int cryptoId, double sum, int fromWalletId, int toWalletId)
        {
            ID = id;
            CreateOn = createon;
            FromClientId = fromClient;
            ToClientId = toClietn;
            CryptoId = cryptoId;
            Sum = sum;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
        }

        //constructor for add
        public Transaction(int id, int fromClient, int toClietn, int cryptoId, double sum, int fromWalletId, int toWalletId)
        {
            ID = id;
            CreateOn = DateTime.Now;
            FromClientId = fromClient;
            ToClientId = toClietn;
            CryptoId = cryptoId;
            Sum = sum;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
        }
    }    
}
