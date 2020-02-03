using System;

namespace Moneybox.App
{
	public class User : IUser
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
