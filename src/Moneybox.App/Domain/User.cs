﻿using System;
using Moneybox.Model;
using Moneybox.Model.Services;

namespace Moneybox.App.Domain
{
	public class User : IUser
	{
		private readonly INotificationService _notificationService;
		
		private const decimal MaxPayInDelta = 500m;
		private static decimal MinBalanceAmount = 500m;
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public User(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		public void NotifyUserUponLowBalance(decimal newBalance)
        {
	        if (newBalance < MinBalanceAmount)
	        {
		        _notificationService.NotifyFundsLow(Email);
	        }
        }

        public void NotifyUserCaseMaxTransferLimitReached(decimal paidIn)
        {
	        if (Account.PayInLimit - paidIn < MaxPayInDelta)
	        {
		        _notificationService.NotifyApproachingPayInLimit(Email);
	        }
        }
	}
}
