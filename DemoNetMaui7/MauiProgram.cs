using DemoNetMaui7.Services;
using DemoNetMaui7.ViewModels;
using DemoNetMaui7.Views;
using Microsoft.Extensions.Logging;

namespace DemoNetMaui7;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiMaps();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IPOIsService, POIsService>();
		builder.Services.AddSingleton<IGoogleService, GoogleService>();

		builder.Services.AddTransient<POIsViewModel>();
		builder.Services.AddTransient<POIsView>();

		builder.Services.AddTransient<FormViewModel>();
		builder.Services.AddTransient<FormView>();

		builder.Services.AddTransient<WebsiteViewModel>();
		builder.Services.AddTransient<WebsiteView>();

		return builder.Build();
	}
}
