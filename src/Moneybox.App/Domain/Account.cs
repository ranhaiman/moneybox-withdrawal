using System;
using Moneybox.Model;

namespace Moneybox.App.Domain
{
	public class Account : IAccount
	{
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public IUser User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public decimal ValidateSufficientFunds(decimal amount)
        {
	        var fromBalance = Balance - amount;
	        if (fromBalance < 0m)
	        {
		        throw new InvalidOperationException("Insufficient funds to make transfer");
	        }

	        return fromBalance;
        }

        public decimal ValidatedMaxPaidInAmount(decimal amount)
        {
	        var paidIn = PaidIn + amount;
	        if (paidIn > PayInLimit)
	        {
		        throw new InvalidOperationException("Account pay in limit reached");
	        }

	        return paidIn;
        }
	}
}
