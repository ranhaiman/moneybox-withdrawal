using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App.Domain;
using Moneybox.App.Features;
using Moneybox.Model;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Services;
using Moq;

namespace MoneyboxTest
{
	[TestClass]
	public class WithdrawMoneyShould
	{
		private IUser _fromUser;
		private Guid _fromAccountId;
		private IAccount _fromAccount;
		private Mock<IAccountRepository> _accountRepositoryMock;
		private Mock<INotificationService> _notificationServiceMock;


		[TestMethod]
		public void WithdrawMoneySuccess()
		{
			var amountToWithdraw = 20;
			var fromBalance = _fromAccount.Balance;
			
			var withdrawMoney = new WithdrawMoney(_accountRepositoryMock.Object);

			withdrawMoney.Execute(_fromAccountId, amountToWithdraw);

			Assert.AreEqual(_fromAccountId, _fromAccount.Id);
			Assert.AreEqual(0, _fromAccount.PaidIn);
			Assert.AreEqual(- amountToWithdraw, _fromAccount.Withdrawn);
			Assert.AreEqual(fromBalance - amountToWithdraw, _fromAccount.Balance);
			_accountRepositoryMock.Verify(x=>x.Update(_fromAccount));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ValidateSufficientWithdrawFundsSuccess()
		{
			var amountToWithdraw = 1001;

			var withdrawMoney = new WithdrawMoney(_accountRepositoryMock.Object);

			withdrawMoney.Execute(_fromAccountId, amountToWithdraw);
		}

		[TestMethod]
		public void NotifyUserUponLowBalance()
		{
			var amountToWithdraw = 950;
			
			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
			var withdrawMoney = new WithdrawMoney(_accountRepositoryMock.Object);

			withdrawMoney.Execute(_fromAccountId, amountToWithdraw);

			_notificationServiceMock.Verify(x=>x.NotifyFundsLow(_fromAccount.User.Email));
			_accountRepositoryMock.Verify(x=>x.Update(_fromAccount));
		}

		[TestInitialize]
		public void Initialize()
		{
			_fromAccountId = Guid.NewGuid();
			
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_notificationServiceMock = new Mock<INotificationService>();

			_fromUser = new User(_notificationServiceMock.Object)
			{
				Email = "a@email.com", Id = Guid.NewGuid(), Name = "UserA"
			};

			_fromAccount = new Account
			{
				User = _fromUser,
				Id = _fromAccountId,
				Balance = 1000
			};

			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
		}
	}
}