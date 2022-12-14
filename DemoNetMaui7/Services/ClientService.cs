using System.Net.Http.Headers;

namespace DemoNetMaui7.Services
{
	public class ClientService
	{
		protected HttpClient CreateClient(string url = "")
		{
			var client = new HttpClient();

			if (!string.IsNullOrEmpty(url) )
				client.BaseAddress = new Uri(url);

			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

			return client;
		}
	}
}
