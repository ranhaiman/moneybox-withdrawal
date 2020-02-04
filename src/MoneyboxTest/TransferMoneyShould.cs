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
	public class TransferMoneyShould
	{
		private IUser _toUser;
		private IUser _fromUser;
		private Guid _toAccountId;
		private Guid _fromAccountId;
		private IAccount _toAccount;
		private IAccount _fromAccount;
		private Mock<IAccountRepository> _accountRepositoryMock;
		private Mock<INotificationService> _notificationServiceMock;


		[TestMethod]
		public void TransferMoneySuccess()
		{
			var amountToTransfer = 20;
			var fromBalance = _fromAccount.Balance;
			var toBalance = _toAccount.Balance;
			
			var transferMoney = new TransferMoney(_accountRepositoryMock.Object);

			transferMoney.Execute(_fromAccountId, _toAccountId, amountToTransfer);

			Assert.AreEqual(_fromAccountId, _fromAccount.Id);
			Assert.AreEqual(0, _fromAccount.PaidIn);
			Assert.AreEqual(- amountToTransfer, _fromAccount.Withdrawn);
			Assert.AreEqual(fromBalance - amountToTransfer, _fromAccount.Balance);

			Assert.AreEqual(_toAccountId, _toAccount.Id);
			Assert.AreEqual(toBalance + amountToTransfer, _toAccount.Balance);

			_accountRepositoryMock.Verify(x=>x.Update(_fromAccount));
			_accountRepositoryMock.Verify(x=>x.Update(_toAccount));
		}

		[TestMethod]
		public void NotifyUserUponLowBalanceSuccess()
		{
			var amountToTransfer = 800;

			var transferMoney = new TransferMoney(_accountRepositoryMock.Object);

			transferMoney.Execute(_fromAccountId, _toAccountId, amountToTransfer);

			_notificationServiceMock.Verify(x=>x.NotifyFundsLow(_fromAccount.User.Email));
			_accountRepositoryMock.Verify(x=>x.Update(_fromAccount));
			_accountRepositoryMock.Verify(x=>x.Update(_toAccount));
		}

		[TestMethod]
		public void NotifyUserCaseMaxTransferLimitReachedSuccess()
		{
			var amountToTransfer = 3800;
			_fromAccount.Balance = 5000;

			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
			var transferMoney = new TransferMoney(_accountRepositoryMock.Object);

			transferMoney.Execute(_fromAccountId, _toAccountId, amountToTransfer);

			_notificationServiceMock.Verify(x=>x.NotifyApproachingPayInLimit(_toAccount.User.Email));
			_accountRepositoryMock.Verify(x=>x.Update(_fromAccount));
			_accountRepositoryMock.Verify(x=>x.Update(_toAccount));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ValidateSufficientFundsSuccess()
		{
			var amountToTransfer = 3800;
			_fromAccount.Balance = 500;

			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
			var transferMoney = new TransferMoney(_accountRepositoryMock.Object);

			transferMoney.Execute(_fromAccountId, _toAccountId, amountToTransfer);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void ValidatedMaxPaidInAmountSuccess()
		{
			var amountToTransfer = 3800;
			_fromAccount.Balance = 500;

			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
			var transferMoney = new TransferMoney(_accountRepositoryMock.Object);

			transferMoney.Execute(_fromAccountId, _toAccountId, amountToTransfer);
		}

		[TestInitialize]
		public void Initialize()
		{
			_fromAccountId = Guid.NewGuid();
			_toAccountId = Guid.NewGuid();

			_accountRepositoryMock = new Mock<IAccountRepository>();
			_notificationServiceMock = new Mock<INotificationService>();

			_fromUser = new User(_notificationServiceMock.Object)
			{
				Email = "a@email.com", Id = Guid.NewGuid(), Name = "UserA"
			};

			_toUser = new User(_notificationServiceMock.Object)
			{
				Email = "b@email.com", Id = Guid.NewGuid(), Name = "UserB"
			};

			_fromAccount = new Account
			{
				User = _fromUser,
				Id = _fromAccountId,
				Balance = 900
			};

			_toAccount = new Account
			{
				Id =_toAccountId,
				User = _toUser,
				Balance = 3600
			};

			_accountRepositoryMock.Setup(x => x.GetAccountById(_fromAccountId)).Returns(_fromAccount);
			_accountRepositoryMock.Setup(x => x.GetAccountById(_toAccountId)).Returns(_toAccount);
		}
	}
}
