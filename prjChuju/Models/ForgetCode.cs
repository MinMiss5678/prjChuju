using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ForgetCode
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string TheCode { get; set; } = null!;

        public virtual AccountInfo Account { get; set; } = null!;
    }
}
