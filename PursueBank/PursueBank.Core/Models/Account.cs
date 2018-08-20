using System;
using System.Collections.Generic;

namespace PursueBank.Core.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }
    }
}
