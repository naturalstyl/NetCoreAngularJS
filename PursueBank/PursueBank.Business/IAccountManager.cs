using PursueBank.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PursueBank.Business
{
    public interface IAccountManager
    {
        IEnumerable<LedgerTransactionsResponse> GetAllTransactionByAccountId(int accountId, DateTime? transactionStartDate = null);
        bool RequestPayment(PaymentRequest request);
    }
}
