using System;

namespace Moneybox.Model.DataAccess
{
    public interface IAccountRepository
    {
        IAccount GetAccountById(Guid accountId);

        void Update(IAccount account);
    }
}
