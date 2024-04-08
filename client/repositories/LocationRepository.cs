using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using client.models;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Windows.Input;
using System.Diagnostics.Eventing.Reader;

namespace client.repositories
{
    internal class LocationRepository
    {
        private Dictionary<string, string> _postsLocations = new Dictionary<string, string>();

        private string _googlePlacesAPIKey;
        private string _searchLocationsURL;
        private string _getPlaceDetailsURL;
        private readonly HttpClient _httpClient;

        public LocationRepository()
        {
            _googlePlacesAPIKey = Environment.GetEnvironmentVariable("GOOGLE_PLACES_API_KEY");
            _searchLocationsURL =
                "https://maps.googleapis.com/maps/api/place/textsearch/json?key="
                + _googlePlacesAPIKey
                + "&query=";
            _getPlaceDetailsURL =
                "https://maps.googleapis.com/maps/api/place/details/json?key="
                + _googlePlacesAPIKey
                + "&fields=name" + "%2C" + "geometry" + "%2C" + "place_id" // add "%2C" between fields
                + "&place_id="
                ;
            _httpClient = new HttpClient();
        }

        public Dictionary<string, string> GetAllPostsLocations()
        {
            return _postsLocations;
        }

        public bool AddPostLocation(string postId, string locationId)
        {
            if (!_postsLocations.ContainsKey(postId))
            {
                _postsLocations.Add(postId, locationId);

                return true;
            }

            return false;
        }

        public bool RemovePostLocation(string postId)
        {
            if (_postsLocations.ContainsKey(postId))
            {

                _postsLocations.Remove(postId);

                return true;
            }

            return false;
        }

        public bool UpdatePostLocation(string postId, string locationId)
        {
            if (_postsLocations.ContainsKey(postId))
            {
                _postsLocations[postId] = locationId;

                return true;
            }

            return false;
        }

        public async Task<List<Location>> SearchLocations(string query)
        {
            string requestURL = _searchLocationsURL + query;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<Location> locations = ParseSearchLocationsJsonResponse(jsonResponse);
                    /*await Task.Delay(1000);*/
                    return locations;
                }
                else
                {
                    // maybe throw an exception here
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        static List<Location> ParseSearchLocationsJsonResponse(string jsonResponse)
        {
            List<Location> locations = new List<Location>();

            JObject root = JObject.Parse(jsonResponse);
            JArray results = (JArray)root["results"];

            foreach (var result in results)
            {
                Location location = new Location(
                    (string)result["place_id"],
                    (string)result["name"],
                    (double)result["geometry"]["location"]["lat"],
                    (double)result["geometry"]["location"]["lng"]
                );
                locations.Add(location);
            }

            return locations;
        }

        public async Task<Location> GetLocationDetails(string locationId)
        {
            string requestURL = _getPlaceDetailsURL + locationId;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestURL);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    /*await Task.Delay(1000);*/
                    return ParseGetLocationDetailsJsonResponse(jsonResponse);
                }
                else
                {
                    // maybe throw an exception here
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        static Location ParseGetLocationDetailsJsonResponse(string jsonResponse)
        {

            JObject root = JObject.Parse(jsonResponse);

            string locationId = (string)root["result"]["place_id"];
            string locationName = (string)root["result"]["name"];
            double locationLatitude = (double)root["result"]["geometry"]["location"]["lat"];
            double locationLongitude = (double)root["result"]["geometry"]["location"]["lng"];

            return new Location(locationId, locationName, locationLatitude, locationLongitude);
        }
    }
}
