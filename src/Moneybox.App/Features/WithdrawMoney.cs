using System;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Features;
using Moneybox.Model.Services;

namespace Moneybox.App.Features
{

	public class WithdrawMoney : IWithdrawMoney
	{
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
        }
    }
}
