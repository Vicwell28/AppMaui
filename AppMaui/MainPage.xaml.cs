using AppMaui.ViewModels;

namespace AppMaui
{
	public partial class MainPage : ContentPage
	{
		public MainPage(MainPageViewModel mainPageViewModel)
		{
			InitializeComponent();
			this.BindingContext = mainPageViewModel;
		}
	}
}
