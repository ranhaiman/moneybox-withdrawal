using System;
using Moneybox.Model;

namespace Moneybox.App.Domain
{
	public class User : IUser
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
