using System;
using Moneybox.Model;
using Moneybox.Model.DataAccess;

namespace Moneybox.DataAccess
{
	public class AccountRepository : IAccountRepository
	{
		public IAccount GetAccountById(Guid accountId)
		{
			throw new NotImplementedException();
		}

		public void Update(IAccount account)
		{
			throw new NotImplementedException();
		}
	}
}