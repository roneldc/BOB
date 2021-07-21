using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AccompProject.Hubs
{
    public class BingoHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}