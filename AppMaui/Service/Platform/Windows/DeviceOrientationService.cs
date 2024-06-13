using AppMaui.Service.Contracts;

namespace AppMaui.Service.Implementations
{
	public partial class DeviceOrientationService
    {
        public partial DeviceOrientation GetOrientation()
        {
            return DeviceOrientation.Portrait;
        }
    }
}
