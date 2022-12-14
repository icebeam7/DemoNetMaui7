using System.Text.Json;

using DemoNetMaui7.Models;
using DemoNetMaui7.Helpers;

namespace DemoNetMaui7.Services
{
	public class GoogleService : ClientService, IGoogleService
	{
		private HttpClient googleClient;

		public GoogleService()
		{
			googleClient = CreateClient(Constants.GoogleMapsApiUrl);
		}

		public async Task<GoogleDirection> GetDirections(Location origin, Location destination)
		{
			var directions = new GoogleDirection();

			var service = $"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={origin.Latitude},{origin.Longitude}&destination={destination.Latitude},{destination.Longitude}&key={Constants.GoogleMapsApiKey}";
			var response = await googleClient.GetAsync(service);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				directions = JsonSerializer.Deserialize<GoogleDirection>(json);
			}

			return directions;
		}
	}
}
