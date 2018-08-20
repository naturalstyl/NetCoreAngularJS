using PursueBank.Data;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PursueBank.Core.Models;
using System.Collections.Generic;
using PursueBank.Business.Validators;

namespace PursueBank.Business
{
    public class AccountManager : IAccountManager
    {
        private PursueContext _pursueContext;

        public AccountManager(PursueContext context)
        {
            _pursueContext = context;

            FinalizeTodaysPendingTransactions();
        }

        private void FinalizeTodaysPendingTransactions()
        {
            var todaysPendingTrans = _pursueContext.PendingTransactions.Where(pt => pt.PayDate.Date == DateTime.Today.Date).ToList();
            if (todaysPendingTrans.Count > 0)
            {
                foreach (var trans in todaysPendingTrans)
                {
                    var account = _pursueContext.Accounts.Where(a => a.Id == trans.AccountId).Single();
                    account.Balance = account.Balance - trans.Amount;

                    _pursueContext.Transactions.Add(new Transaction()
                    {
                        AccountId = trans.AccountId,
                        Amount = trans.Amount,
                        TransactionBalance = account.Balance,
                        TransactionTypeId = 1,
                        Description = trans.Description,
                        WhenTransacted = DateTime.Now
                    });

                    _pursueContext.Entry(trans).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                    _pursueContext.SaveChanges();
                }
            }
        }

        public IEnumerable<LedgerTransactionsResponse> GetAllTransactionByAccountId(int accountId, DateTime? transactionStartDate = null)
        {
            var at = _pursueContext.Transactions
                .Where(t => t.AccountId == accountId);

            if (transactionStartDate != null)
            {
                at = at.Where(t => t.WhenTransacted >= transactionStartDate.Value.Date);
            }

            var query = at.Select(trans => new LedgerTransactionsResponse()
            {
                TransactionId = trans.Id,
                WhenTransacted = trans.WhenTransacted,
                TransactionBalance = trans.TransactionBalance,
                Description = trans.Description,
                Amount = trans.TransactionType.Name.ToLower().Equals("credit") ? trans.Amount : -trans.Amount,
                AccountId = trans.AccountId,
                TransactionTypeId = trans.TransactionTypeId,
                TransactionTypeName = trans.TransactionType.Name
            }).OrderBy(t => t.WhenTransacted);

            return query.ToList();
        }

        public bool RequestPayment(PaymentRequest request)
        {
            var validator = new RequestPaymentValidator();

            var valResults = validator.Validate(request);

            if (!valResults.IsValid)
            {
                throw new ArgumentException($"Failed payment request validation. {valResults.ToString()}");
            }

            // Check if pending need to be paid first
            if (request.SendByDate.Date > DateTime.Today.Date)
            {
                var pendingPayment = new PendingTransaction()
                {
                    AccountId = request.AccountId,
                    TransactionTypeId = 1,
                    Amount = request.Amount,
                    PayDate = request.SendByDate,
                    Description = request.Description
                };

                _pursueContext.PendingTransactions.Add(pendingPayment);

                _pursueContext.SaveChanges();
            }
            else
            {
                // If today payment substract payment amount from account balance
                // then take that balance amount and insert transaction record
                var account = _pursueContext.Accounts.Where(a => a.Id == request.AccountId).Single();

                account.Balance = account.Balance - request.Amount;
                _pursueContext.Transactions.Add(new Transaction()
                {
                    AccountId = account.Id,
                    Amount = request.Amount,
                    TransactionBalance = account.Balance,
                    TransactionTypeId = 1,
                    Description = request.Description,
                    WhenTransacted = DateTime.Now
                });

                _pursueContext.SaveChanges();
            }

            return true;
        }
    }
}
