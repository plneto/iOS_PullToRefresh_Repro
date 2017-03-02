using Xamarin.Forms;

namespace ios_pulltorefresh_repro
{
	public partial class ios_pulltorefresh_reproPage : ContentPage
	{
		public ios_pulltorefresh_reproPage()
		{
			InitializeComponent();
			BindingContext = new MainViewModel();
		}
	}
}
