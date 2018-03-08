using Eventaris.Domain;
using EventarisXDal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace EventarisXamarin.ViewModel
{
    public class NewProfileViewModel : ViewModelBase
    {
        private static ISettings AppSettings =>
            CrossSettings.Current;

        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;

        private User _user;
        public User User
        {
            get { return _user; }
            set
            {
                _user = value; 
                RaisePropertyChanged(nameof(User));
            }
        }

        public NewProfileViewModel(IUserService userService, INavigationService navigationService)
        {
            _userService = userService;
            _navigationService = navigationService;           
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(
                           async () =>
                           {
                               var existingUser = _userService.GetByName(User.FirstName, User.LastName);
                               if (existingUser != null)
                               {
                                   User = existingUser;
                               }
                               else
                               {
                                   var result = await _userService.New(User);
                                   if (result == false)
                                   {
                                       var dialog = SimpleIoc.Default.GetInstance<IDialogService>();
                                       await
                                           dialog.ShowError(
                                               "Error when saving, the change was not saved",
                                               "Error",
                                               "OK",
                                               null);
                                   }
                                   User =  _userService.GetByName(User.FirstName, User.LastName);
                               }                              
                               AppSettings.AddOrUpdateValue(nameof(User.Id), User.Id.ToString());

                               _navigationService.GoBack();
                           }));
                

            }
        }


    }
}
