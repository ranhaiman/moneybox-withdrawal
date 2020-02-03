using System;

namespace Moneybox.App
{
	public class Account : IAccount
	{
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public IUser User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }
    }
}
