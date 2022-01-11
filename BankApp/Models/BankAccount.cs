using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class BankAccount
    {
        private int? id { get; set; }
        private int? customer_id { get; set; }
        private string? number { get; set; }
        private string? description { get; set; }

        public BankAccount()
        {
            this.ID = null;
            this.CustomerID = null;
            this.Number = null;
            this.Description = null;
        }

        public BankAccount(int? id, int? customerId, string? number, string? description)
        {
            this.ID = id;
            this.CustomerID = customerId;
            this.Number = number;
            this.Description = description;
        }

        public int? ID
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public int? CustomerID
        {
            get { return customer_id; }
            set
            {
                customer_id = value;
            }
        }
        public string? Number
        {
            get { return number; }
            set
            {
                number = value;
            }
        }
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
            }
        }
    }
}