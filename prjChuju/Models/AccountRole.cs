using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class AccountRole
    {
        public AccountRole()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Role { get; set; } = null!;

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
