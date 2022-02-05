using Practice.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice
{
    public class ConvertCrypto
    {
        public int ID { get; set; }
        public int ClientId { get; set; }
        public int FromCryptoId { get; set; }
        public int ToCryptoId { get; set; }
        public double FromSum { get; set; }
        public double ToSum { get; set; }
        public int FromWalletId { get; set; }
        public int ToWalletId { get; set; }

        //for all
        public ConvertCrypto(int id, int clientId, int fromCryptoId, int toCryptoId, 
            double fromSum, int fromWalletId, int toWalletId)
        {
            ID = id;
            ClientId = clientId;
            FromCryptoId = fromCryptoId;
            ToCryptoId = toCryptoId;
            FromSum = fromSum;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
        }

        public ConvertCrypto(int id, int clientId, int fromCryptoId, int toCryptoId,
            double fromSum, double toSum, int fromWalletId, int toWalletId)
        {
            ID = id;
            ClientId = clientId;
            FromCryptoId = fromCryptoId;
            ToCryptoId = toCryptoId;
            FromSum = fromSum;
            ToSum = toSum;
            FromWalletId = fromWalletId;
            ToWalletId = toWalletId;
        }
    }
}
