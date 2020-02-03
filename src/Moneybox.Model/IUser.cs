using System;

namespace Moneybox.Model
{
	public interface IUser
	{
		Guid Id { get; set; }
		string Name { get; set; }
		string Email { get; set; }

		void NotifyUserUponLowBalance(decimal newBalance);
		void NotifyUserCaseMaxTransferLimitReached(decimal paidIn);
	}
}