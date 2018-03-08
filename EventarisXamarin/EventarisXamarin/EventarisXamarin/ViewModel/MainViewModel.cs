using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Eventaris.Domain;
using EventarisXamarin.Services;
using EventarisXDal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace EventarisXamarin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static ISettings AppSettings =>
            CrossSettings.Current;

        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        private RelayCommand<EditProfileViewModel> _editProfileCommand;
        private RelayCommand _scanCommand;

        private string _welcomeText;
        public string WelcomeText
        {
            get { return _welcomeText;}
            set
            {
                _welcomeText = value;
                RaisePropertyChanged(nameof(WelcomeText));
            }
        }

        private User _user;
        public User User
        {
            get { return _user;}
            set
            {
                _user = value;
                WelcomeText = "Welcome " + User.FirstName;
                RaisePropertyChanged(nameof(User));
            }
        }


        public MainViewModel(INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;

        }

        public void ConnectUser()
        {
            //Clear Appsettings for testing purposes
            //AppSettings.Remove(nameof(User.Id));

            Debug.Print(AppSettings.Contains(nameof(User.Id)).ToString());

            if (!AppSettings.Contains(nameof(User.Id)))
            {
                User newUser = new User();
                Messenger.Default.Send(new NotificationMessage<User>(newUser, "New"));
                _navigationService.NavigateTo(ViewModelLocator.NewProfilePageKey, newUser);
            }
            else
            {
                var currentId = AppSettings.GetValueOrDefault(nameof(User.Id), string.Empty);
                if (currentId.Equals(String.Empty))
                {
                    Debug.Print("internal id is emtpy");
                }else
                {
                    User = _userService.GetById(Int32.Parse(currentId.ToString()));
                }
            }
        }

        public RelayCommand<EditProfileViewModel> EditProfileCommand
        {
            get
            {
                return _editProfileCommand ?? (_editProfileCommand = new RelayCommand<EditProfileViewModel>(
                           (user) =>
                           {
                               if (User==null)
                               {
                                   _navigationService.NavigateTo(ViewModelLocator.EditProfilePageKey, User);
                               }
                               _navigationService.NavigateTo(ViewModelLocator.EditProfilePageKey, User);
                           }));  
            }
        }

        public RelayCommand ScanCommand => _scanCommand ?? (_scanCommand = new RelayCommand( ()=>
                           _navigationService.NavigateTo(ViewModelLocator.CustomScanPageKey, User)));

    }
}
