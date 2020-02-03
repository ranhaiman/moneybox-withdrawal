using System;
using Microsoft.Extensions.DependencyInjection;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moneybox.DataAccess;
using Moneybox.Model.DataAccess;
using Moneybox.Model.Features;
using Moneybox.Model.Services;


namespace Moneybox.App
{
	public class ComponentInitialize
	{
		public IServiceProvider ServiceProvider { get; }
		private IServiceProvider _serviceProvider;
		private ServiceCollection _serviceCollection;
		private static volatile ComponentInitialize _instance;
		
		private ComponentInitialize()
		{
			RegisterServices();
			ServiceProvider = _serviceProvider;
		}

		public static ComponentInitialize Instance
			=> _instance ??= new ComponentInitialize();

		private void RegisterServices()
		{
			_serviceCollection = new ServiceCollection();
			_serviceCollection.AddSingleton<INotificationService, NotificationService>();
			_serviceCollection.AddSingleton<ITransferMoney, TransferMoney>();
			_serviceCollection.AddSingleton<IWithdrawMoney, WithdrawMoney>();
			_serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();

			_serviceProvider = _serviceCollection.BuildServiceProvider();
		}
	}
}
