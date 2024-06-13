using System;
using Microsoft.Extensions.DependencyInjection;

namespace AppMaui.Service
{
    public static class DeviceServiceFactory
    {
        public static IDeviceService Create(IServiceProvider serviceProvider)
        {
            System.Diagnostics.Debug.WriteLine("DeviceServiceFactory.Create");

            #if ANDROID
                return serviceProvider.GetRequiredService<AndroidDeviceService>();
            #elif IOS
                return serviceProvider.GetRequiredService<IOSDeviceService>();
            #elif WINDOWS
                return serviceProvider.GetRequiredService<WindowsDeviceService>();
            #else
                throw new PlatformNotSupportedException("Platform not supported");
            #endif
        }
    }
}