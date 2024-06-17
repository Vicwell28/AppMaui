using AppMaui.ViewModels;
using AppMaui.Service.Contracts;
using AppMaui.Service.Implementations;
using AppMaui.Helpers;
using AppMaui.Page;

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

			builder.RegisterAppTranslation();

			builder.Services.AddTransient<MainPageViewModel>();
			builder.Services.AddTransient<MainPage>();
			builder.Services.AddTransient<AboutPage>();
            builder.Services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
			builder.Services.AddSingleton<IDeviceService, DeviceService>();

			return builder.Build();
		}

		public static MauiAppBuilder RegisterAppTranslation(this MauiAppBuilder mauiAppBuilder)
		{
			// Configuración de traducciones.
			Dsd.Infrastructure.Translations.Instance.Init();
			ResourceManagerProvider resourceManager = new ResourceManagerProvider(new AppDomainProvider(), "Dsd.Infrastructure.Translations.Resources");
			mauiAppBuilder.Services.AddSingleton<IResourceManagerProvider>(resourceManager);
			mauiAppBuilder.Services.AddSingleton<ICultureProvider, CultureProvider>();
			mauiAppBuilder.Services.AddSingleton<ITranslationService, ResxTranslationService>();
			return mauiAppBuilder;
		}
	}
}
