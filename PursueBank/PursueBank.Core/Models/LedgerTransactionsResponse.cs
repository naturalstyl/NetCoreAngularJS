using System;
using System.Collections.Generic;
using System.Text;

namespace PursueBank.Core.Models
{
    public class LedgerTransactionsResponse
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime WhenTransacted { get; set; }
        public string Description { get; set; }
        public decimal TransactionBalance { get; set; }
    }
}