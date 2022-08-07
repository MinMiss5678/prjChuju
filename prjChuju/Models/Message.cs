using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int? ClientId { get; set; }

        public virtual ClientConnection? Client { get; set; }
    }
}
