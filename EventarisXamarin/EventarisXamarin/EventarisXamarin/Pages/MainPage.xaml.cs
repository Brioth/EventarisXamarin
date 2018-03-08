using EventarisXamarin.ViewModel;
using Xamarin.Forms;

namespace EventarisXamarin.Pages
{
	public partial class MainPage : ContentPage
	{
	    public MainViewModel Vm
	    {
	        get { return (MainViewModel) BindingContext; }
	    }

		public MainPage()
		{
			InitializeComponent();
            

		    BindingContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).Main;

		}

	    protected override void OnAppearing()
	    {
	        this.Vm.ConnectUser();
	    }
	}
}
