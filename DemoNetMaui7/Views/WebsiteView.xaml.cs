using DemoNetMaui7.ViewModels;

namespace DemoNetMaui7.Views;

public partial class WebsiteView : ContentPage
{
	WebsiteViewModel vm;

	public WebsiteView(WebsiteViewModel vm)
	{
		InitializeComponent();
		
		this.vm = vm;
		this.BindingContext = vm;
	}
}