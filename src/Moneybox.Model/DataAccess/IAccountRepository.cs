using System;

namespace Moneybox.App.DataAccess
{
    public interface IAccountRepository
    {
        IAccount GetAccountById(Guid accountId);

        void Update(IAccount account);
    }
}
