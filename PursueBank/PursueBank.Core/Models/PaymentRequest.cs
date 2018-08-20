using System;
using System.Collections.Generic;
using System.Text;

namespace PursueBank.Core.Models
{
    public class PaymentRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime SendByDate { get; set; }

        public string Description { get; set; }
    }
}
