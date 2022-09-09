using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public int Role { get; set; }

        public virtual AccountRole RoleNavigation { get; set; } = null!;
    }
}
