using System;

namespace Moneybox.Model.Features
{
	public interface IWithdrawMoney
	{
		void Execute(Guid fromAccountId, decimal amount);
	}
}