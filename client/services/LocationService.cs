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

        // Returns the Location of one post
        public async Task<Location> GetPostLocation(string postId)
        {
            Dictionary<string, string> allPostsLocations = _locationRepository.GetAllPostsLocations();
            string locationId;

            if (allPostsLocations.ContainsKey(postId))
            {
                locationId = allPostsLocations[postId];
            }
            else
            {
                return null;
            }

            // Create Location object by requesting the API
            return await _locationRepository.GetLocationDetails(locationId);
        }

        public async Task<Location> GetLocationById(string locationId)
        {
            return await _locationRepository.GetLocationDetails(locationId);
        }

        public void AddPostLocation(string postId, string locationId)
        {
            _locationRepository.AddPostLocation(postId, locationId);
        }

        public void RemovePostLocation(string postId)
        {
            _locationRepository.RemovePostLocation(postId);
        }

        public void UpdatePostLocation(string postId, string locationId)
        {
            _locationRepository.UpdatePostLocation(postId, locationId);
        }
    }
}
