using System;

namespace Moneybox.App
{
	public interface IUser
	{
		Guid Id { get; set; }
		string Name { get; set; }
		string Email { get; set; }
	}
}