using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class BankAccount
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }

        public BankAccount()
        {
            Id = null;
            CustomerId = null;
            Number = null;
            Description = null;
        }

        public BankAccount(int? id, int? customerId, string? number, string? description)
        {
            Id = id;
            CustomerId = customerId;
            Number = number;
            Description = description;
        }
    }
}