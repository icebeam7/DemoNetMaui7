using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNetMaui7.ViewModels
{
	public partial class WebsiteViewModel : BaseViewModel
	{
		[ObservableProperty]
		string url = "https://learn.microsoft.com/dotnet/maui";

		[RelayCommand]
		private void GoToWebsite(string repo)
		{
			Url = $"https://github.com/dotnet/{repo}";
		}
	}
}
