using AppMaui.Service;
using AppMaui.ViewModels;
using Microsoft.Extensions.Logging;

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

        #if DEBUG
    		        builder.Logging.AddDebug();
        #endif

        #if ANDROID
                    builder.Services.AddSingleton<AndroidDeviceService>();
        #endif

        #if IOS
                    builder.Services.AddSingleton<IOSDeviceService>();
        #endif

        #if WINDOWS
                    builder.Services.AddSingleton<WindowsDeviceService>();
        #endif

            builder.Services.AddSingleton<IDeviceService>(sp => DeviceServiceFactory.Create(sp));
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
