using Microsoft.Maui.Controls.Maps;
using DemoNetMaui7.ViewModels;

namespace DemoNetMaui7.Views;

public partial class POIsView : ContentPage
{
	POIsViewModel vm;

	public POIsView(POIsViewModel vm)
	{
		InitializeComponent();

		this.vm = vm;
		BindingContext = vm;
	}

	protected override void OnDisappearing()
	{
		vm.DisposeCancellationTokenCommand.Execute(null);

		base.OnDisappearing();
	}

	private async void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
	{
		e.HideInfoWindow = true;

		var pin = sender as Pin;
		var destination = pin.Location;

		await vm.GetDirectionsToPOICommand.ExecuteAsync(destination);
	}
}