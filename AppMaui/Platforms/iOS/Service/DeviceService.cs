using UIKit;

namespace AppMaui.Service
{
    public class IOSDeviceService : IDeviceService
    {
        public string GetDeviceModel()
        {
            return UIDevice.CurrentDevice.Model;
        }
    }
}