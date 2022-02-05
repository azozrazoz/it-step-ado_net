using System;
using System.Collections.Generic;
using System.Text;

namespace Practice
{
    public class Wallet
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CryptoId { get; set; }
        public double CryptoAmount { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime DeleteOn { get; set; }
        public bool IsDeleted { get; set; }

        //constructor to add
        public Wallet(int id, int clientId, int cryptoId)
        {
            Id = id;
            ClientId = clientId;
            CryptoId = cryptoId;
            CryptoAmount = 0;
            CreateOn = DateTime.Now;
            UpdateOn = CreateOn;
            DeleteOn = CreateOn;
        }

        //constructor to get
        public Wallet(int id, int clientId, int cryptoId, double cryptoAmount, 
            DateTime createOn, DateTime updateOn, DateTime deletedOn, bool isDeleted)
        {
            Id = id;
            ClientId = clientId;
            CryptoId = cryptoId;
            CryptoAmount = cryptoAmount;
            CreateOn = createOn;
            UpdateOn = updateOn;
            DeleteOn = deletedOn;
            IsDeleted = isDeleted;
        }
    }
}
