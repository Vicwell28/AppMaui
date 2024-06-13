using Android.OS;

namespace AppMaui.Service
{
    public class AndroidDeviceService : IDeviceService
    {
        public string GetDeviceModel()
        {
            return Build.Model ?? "Nada";
        }
    }
}