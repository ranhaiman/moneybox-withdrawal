using Moneybox.App.Domain.Services;
using System;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Features;
using Moneybox.Model.Services;

namespace Moneybox.App.Features
{

	public class WithdrawMoney : IWithdrawMoney
	{
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
        }
    }
}
