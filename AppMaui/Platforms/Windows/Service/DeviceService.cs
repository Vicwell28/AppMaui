using Windows.System.Profile;

namespace AppMaui.Service
{
    public class WindowsDeviceService : IDeviceService
    {
        public string GetDeviceModel()
        {
            return "Windows";
        }
    }
}