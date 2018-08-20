using System;
using System.Collections.Generic;

namespace PursueBank.Core.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionBalance { get; set; }
        public DateTime WhenTransacted { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }

        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
