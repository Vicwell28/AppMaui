using AppMaui.ViewModels;

namespace AppMaui.Page
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
