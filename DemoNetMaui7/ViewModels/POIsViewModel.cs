using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoNetMaui7.Models;
using DemoNetMaui7.Services;

namespace DemoNetMaui7.ViewModels
{
	public partial class POIsViewModel : BaseViewModel
	{
		POI currentLocation;

		[ObservableProperty]
		bool isReady;

		[ObservableProperty]
		ObservableCollection<POI> pOIs;

		[ObservableProperty]
		GoogleDirection googleDirections;

		public ObservableCollection<POI> POIsCollection { get; } = new();

		private CancellationTokenSource cts;
		private IGeolocation geolocation;
		private IGoogleService googleService;
		private IPOIsService poisService;

		public POIsViewModel(IGeolocation geolocation, IPOIsService poisService, IGoogleService googleService)
		{
			this.geolocation = geolocation;
			this.googleService = googleService;
			this.poisService = poisService;
		}

		[RelayCommand]
		private async Task GetCurrentLocationAsync()
		{
			try
			{
				IsBusy = true;

				cts = new ();

				var request = new GeolocationRequest(
					GeolocationAccuracy.Medium,
					TimeSpan.FromSeconds(10));

				var location = await geolocation.GetLocationAsync(request, cts.Token);
				currentLocation = new(location, "Current Location");

				var poi = new List<POI> { currentLocation };

				POIs = new (poi);
				IsReady = true;
			}
			catch (Exception ex)
			{
				// Unable to get location
			}
			finally
			{
				IsBusy = false;
			}
		}

		[RelayCommand]
		private async Task GetPOIs()
		{
			var pois = await poisService.GetItems<POI>();

			POIsCollection.Clear();

			foreach (var p in pois)
				POIsCollection.Add(p);

			if (currentLocation is not null)
				POIsCollection.Add(currentLocation);
		}

		[RelayCommand]
		private async Task GetDirectionsToPOI(Location selectedPOI)
		{
			if (selectedPOI is not null)
			{
				var directions = await googleService.GetDirections(
					currentLocation.GeoPosition, selectedPOI);

				if (directions != null)
					GoogleDirections = directions;
			}
		}

		[RelayCommand]
		private void DisposeCancellationToken()
		{
			if (cts is not null && !cts.IsCancellationRequested)
				cts.Cancel();
		}
	}
}