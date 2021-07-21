using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AccompProject.Hubs
{
     [HubName("mapping")]
    public class MappingHub : Hub
    {
         public void mapp(string lat, string longi)
         {
             try
             {
                 var mappingHub = GlobalHost.ConnectionManager.GetHubContext<MappingHub>();

                 var hubEventParameters = new
                 {
                     Latitude = lat,
                     Longitude = longi
                 };

                 mappingHub.Clients.All.locationReceived(hubEventParameters);
             }
             catch (Exception ex)
             {
                 ex.ToString();
             }
         }
    }
}