using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using client.models;
using client.repositories;

namespace client.services
{
    internal class LocationService
    {
        private LocationRepository _locationRepository;

        public LocationService(LocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> GetLocationById(String locationId)
        {
            return await _locationRepository.GetLocationDetails(locationId);
        }
    }
}
