using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ServiceConnection
    {
        public ServiceConnection()
        {
            ClientConnections = new HashSet<ClientConnection>();
        }

        public int Id { get; set; }
        public string? ConnectionId { get; set; }
        public int OnlineCount { get; set; }

        public virtual ICollection<ClientConnection> ClientConnections { get; set; }
    }
}
