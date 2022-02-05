using System;
using System.Collections.Generic;
using System.Text;

namespace Practice
{
    public class Crypto
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public DateTime? DeleteOn { get; set; }
        public bool IsDeleted { get; set; }
        public int VersionRow { get; set; }
        public string Name { get; set; }

        //constructor for add
        public Crypto(int id, double price, string name)
        {
            ID = id;
            Price = price;
            CreateOn = DateTime.Now;
            UpdateOn = CreateOn;
            DeleteOn = CreateOn;
            IsDeleted = false;
            VersionRow = 1;
            Name = name;
        }

        //constructor for get 
        public Crypto(int id, double price, 
            DateTime createOn, DateTime upadateOn, DateTime deleteOn, int vRow, bool isDeleted, string name)
        {
            ID = id;
            Price = price;
            CreateOn = createOn;
            UpdateOn = upadateOn;
            DeleteOn = deleteOn;
            IsDeleted = isDeleted;
            VersionRow = vRow;
            Name = name;
        }
    }
}
