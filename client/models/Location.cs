using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class Location
    {
        private String googlePlacesPlaceId;
        private double latitude;
        private double longitude;
        public Location(String googlePlacesPlaceId, double latitude, double longitude)
        {
            this.googlePlacesPlaceId = googlePlacesPlaceId;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
