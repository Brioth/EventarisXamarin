using EventarisXamarin.Pages;
using EventarisXamarin.Services;
using EventarisXDal;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using View = Android.Views.View;

namespace EventarisXamarin
{
	public partial class App : Application
	{
		public App ()
		{
            InitializeComponent();

		    var nav = new NavigationService();
		    nav.Configure(ViewModelLocator.EditProfilePageKey, typeof(EditProfilePage));
            nav.Configure(ViewModelLocator.NewProfilePageKey, typeof(NewProfilePage));
            nav.Configure(ViewModelLocator.CustomScanPageKey, typeof(CustomScanPage));
            SimpleIoc.Default.Register<INavigationService>(()=>nav);

            var userService = new UserService();
		    SimpleIoc.Default.Register<IUserService>(()=>userService);

            var participationService = new ParticipationService();
            SimpleIoc.Default.Register<IParticipationService>(()=>participationService);

            var dialog = new DialogService();
            SimpleIoc.Default.Register<IDialogService>(()=>dialog);

            var navPage = new NavigationPage(new MainPage());

		    nav.Initialize(navPage);
		    dialog.Initialize(navPage);

		    MainPage = navPage;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
