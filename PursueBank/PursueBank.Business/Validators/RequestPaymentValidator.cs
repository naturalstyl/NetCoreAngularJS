using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using PursueBank.Core.Models;

namespace PursueBank.Business.Validators
{
    public class RequestPaymentValidator: AbstractValidator<PaymentRequest>
    {
        public RequestPaymentValidator()
        {
            RuleFor(r => r.AccountId).NotEmpty();
            RuleFor(r => r.Amount).GreaterThan(0);
            RuleFor(r => r.SendByDate).Must(AValidSendDate).WithMessage("Pay Date can only be today or in the future.");
            RuleFor(r => r.Description).NotEmpty().WithMessage("Please enter payment desciption.");
        }

        private bool AValidSendDate(DateTime sendDate)
        {
            if (sendDate < DateTime.Today)
            {
                return false;
            }

            return true;
        }
    }
}
