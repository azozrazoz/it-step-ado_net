using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.Repository
{
    public class ClientEntity
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime DeleteOn { get; set; }
        public bool IsDeleted { get; set; }


        //Constructor to add
        public ClientEntity(int id, string name)
        {
            ID = id;
            FullName = name;
            CreateOn = DateTime.Now;
            UpdateOn = CreateOn;
            DeleteOn = UpdateOn;
            IsDeleted = false;
        }

        //constructor to get
        public ClientEntity(int id, string name, DateTime createOn, DateTime updateOn, DateTime deletedOn, bool isDeleted)
        {
            ID = id;
            FullName = name;
            CreateOn = createOn;
            UpdateOn = updateOn;
            DeleteOn = deletedOn;
            IsDeleted = isDeleted;
        }

        public void Print()
        {
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Full name: {FullName}");
            Console.WriteLine($"Created: {CreateOn}");
            Console.WriteLine($"Updated: {UpdateOn}");
            Console.WriteLine($"Deleted: {DeleteOn}");
            Console.WriteLine($"Is deleted: {IsDeleted}");
            Console.WriteLine("_________________________");
        }
    }
}
