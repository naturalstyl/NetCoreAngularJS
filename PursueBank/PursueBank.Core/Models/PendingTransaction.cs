using System;
using System.Collections.Generic;
using System.Text;

namespace PursueBank.Core.Models
{
    public class PendingTransaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }
        public string Description { get; set; }

        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
