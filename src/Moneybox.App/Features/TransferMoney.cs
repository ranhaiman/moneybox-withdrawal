using System;
using Moneybox.Model;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Features;

namespace Moneybox.App.Features
{
	public class TransferMoney : ITransferMoney
	{
		private readonly IAccountRepository _accountRepository;

        public TransferMoney(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccountById(fromAccountId);
            var toAccount = _accountRepository.GetAccountById(toAccountId);

            var newBalance = fromAccount.ValidateSufficientFunds(amount);

            fromAccount.User.NotifyUserUponLowBalance(newBalance);

            var paidIn = toAccount.ValidatedMaxPaidInAmount(amount);

            toAccount.User.NotifyUserCaseMaxTransferLimitReached(paidIn);

            UpdateSendingAccount(amount, fromAccount);

            UpdatedReceivingAccount(amount, toAccount);

            _accountRepository.Update(fromAccount);
            _accountRepository.Update(toAccount);
        }

        private static void UpdatedReceivingAccount(decimal amount, IAccount toAccount)
        {
	        toAccount.Balance = toAccount.Balance + amount;
	        toAccount.PaidIn = toAccount.PaidIn + amount;
        }

        private static void UpdateSendingAccount(decimal amount, IAccount fromAccount)
        {
	        fromAccount.Balance = fromAccount.Balance - amount;
	        fromAccount.Withdrawn = fromAccount.Withdrawn - amount;
        }
	}
}
