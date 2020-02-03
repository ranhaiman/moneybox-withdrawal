using System;

namespace Moneybox.Model.Features
{
	public interface ITransferMoney
	{
		void Execute(Guid fromAccountId, Guid toAccountId, decimal amount);
	}
}