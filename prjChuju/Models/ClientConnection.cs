using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ClientConnection
    {
        public ClientConnection()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public int? ServiceId { get; set; }

        public virtual ServiceConnection? Service { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
