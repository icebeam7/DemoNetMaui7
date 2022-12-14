using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoNetMaui7.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		bool isBusy;

		[ObservableProperty]
		string title;
	}
}
