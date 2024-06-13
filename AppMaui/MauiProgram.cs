using AppMaui.ViewModels;
using AppMaui.Service.Contracts;
using AppMaui.Service.Implementations;

namespace AppMaui
{
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
				});

			builder.Services.AddTransient<MainPageViewModel>();
			builder.Services.AddTransient<MainPage>();
			builder.Services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
			builder.Services.AddSingleton<IDeviceService, DeviceService>();

			return builder.Build();
		}
	}
}
