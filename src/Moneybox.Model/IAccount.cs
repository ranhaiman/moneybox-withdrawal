using System;

namespace Moneybox.Model
{
	public interface IAccount
	{
		Guid Id { get; set; }
		IUser User { get; set; }
		decimal Balance { get; set; }
		decimal Withdrawn { get; set; }
		decimal PaidIn { get; set; }
	}
}