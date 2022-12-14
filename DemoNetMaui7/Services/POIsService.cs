using DemoNetMaui7.Helpers;
using System.Text.Json;

namespace DemoNetMaui7.Services
{
	public class POIsService : ClientService, IPOIsService
	{
		private static JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		private HttpClient client;

		public POIsService()
		{
			client = CreateClient();
		}

		public async Task<List<T>> GetItems<T>()
		{
			var list = new List<T>();

			if (Connectivity.NetworkAccess == NetworkAccess.Internet)
			{
				var response = await client.GetAsync(Constants.POIsUrl);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStreamAsync();
					list = await JsonSerializer.DeserializeAsync<List<T>>(content, options);
				}
			}

			return list;
		}
	}
}
