using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DemoNetMaui7.ViewModels
{
	public partial class FormViewModel : BaseViewModel
	{
		[ObservableProperty]
		string description;

		[ObservableProperty]
		string pointerInformation = "Something will happen here...";

		[ObservableProperty]
		int width = 250;

		[RelayCommand]
		private async Task SaveAsync()
		{

		}

		[RelayCommand]
		private void Enter()
		{
			AddText("You have entered the image");
		}

		[RelayCommand]
		private void Exit()
		{
			AddText("You have left the image");
		}

		[RelayCommand]
		private void Move()
		{
			AddText("You are moving across the image");
		}

		[RelayCommand]
		private void Click()
		{
			AddText("You have double-clicked the image");
			Width -= 10;
		}

		[RelayCommand]
		private void RightClick()
		{
			AddText("Right click detected!");
			Width += 10;
		}

		[RelayCommand]
		private void IncreaseSize(string w)
		{
			Width += int.Parse(w);
		}

		[RelayCommand]
		private void Refresh()
		{
			AddText("Refresh Command!");
		}

		[RelayCommand]
		private void ResetSize()
		{
			Width = 250;
		}

		void AddText(string text)
		{
			PointerInformation = $"{text} {Environment.NewLine} {pointerInformation}";
		}
	}
}
