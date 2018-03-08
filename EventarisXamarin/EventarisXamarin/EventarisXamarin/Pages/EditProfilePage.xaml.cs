using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventaris.Domain;
using EventarisXamarin.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventarisXamarin.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{
		public EditProfilePage (User user)
		{
			InitializeComponent();

		    BindingContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).Edit;
		    Vm.User = user;
		}

	    public EditProfileViewModel Vm
	    {
	        get { return (EditProfileViewModel)BindingContext; }
	    }
    }
}