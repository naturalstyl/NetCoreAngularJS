using System;
using System.Collections.Generic;

namespace PursueBank.Core.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }
    }
}
