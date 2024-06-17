namespace AppMaui
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
            Routing.RegisterRoute(nameof(Page.NotePage), typeof(Page.NotePage));
        }
    }
}
