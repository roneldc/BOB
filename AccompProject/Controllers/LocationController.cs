using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AccompProject.Hubs;
using Microsoft.AspNet.SignalR;


namespace AccompProject.Controllers
{
    public class LocationController : ApiController
    {

        // POST: api/Location
        [HttpPost]
        public void Post([FromBody] Location location)
        {
            var mappingHub = GlobalHost.ConnectionManager.GetHubContext<MappingHub>();

            var hubEventParameters = new
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            mappingHub.Clients.All.locationReceived(hubEventParameters);
        }

    }
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
