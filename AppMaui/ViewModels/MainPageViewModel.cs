using AppMaui.Service.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppMaui.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IDeviceService _deviceService;
        private string _deviceModel = string.Empty;

        public MainPageViewModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
            DeviceModel = _deviceService.GetDeviceModel();
        }

        public string DeviceModel
        {
            get => _deviceModel;
            set
            {
                if (_deviceModel != value)
                {
                    _deviceModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}