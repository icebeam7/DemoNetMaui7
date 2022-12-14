using DemoNetMaui7.ViewModels;

namespace DemoNetMaui7.Views;

public partial class FormView : ContentPage
{
	FormViewModel vm;

    public FormView(FormViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		this.BindingContext = vm;
	}
}