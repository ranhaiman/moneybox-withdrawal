using System;
using Moneybox.Model;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Features;

namespace Moneybox.App.Features
{
	public class WithdrawMoney : IWithdrawMoney
	{
        private readonly IAccountRepository _accountRepository;
        
        public WithdrawMoney(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
	        var fromAccount = _accountRepository.GetAccountById(fromAccountId);

	        var newBalance = fromAccount.ValidateSufficientWithdrawFunds(amount);

	        fromAccount.User.NotifyUserUponLowBalance(newBalance);

	        UpdateAccount(amount, fromAccount);

	        _accountRepository.Update(fromAccount);
        }

        private static void UpdateAccount(decimal amount, IAccount fromAccount)
        {
	        fromAccount.Balance = fromAccount.Balance - amount;
	        fromAccount.Withdrawn = fromAccount.Withdrawn - amount;
        }
    }
}
