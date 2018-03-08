using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Eventaris.Domain;
using EventarisXDal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZXing;

namespace EventarisXamarin.ViewModel
{
    public class CustomScanViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IParticipationService _participationService;
        private readonly IDialogService _dialogService;

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

        public CustomScanViewModel(INavigationService navigationService, IParticipationService participationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _participationService = participationService;
            _dialogService = dialogService;
        }


        public async Task RegisterParticipation(User user, Result result)
        {
            Participation participation = new Participation();
            participation.EventId = Int32.Parse(result.Text);
            participation.UserId = user.Id;

            var succes = await _participationService.RegisterParticipation(participation);

            if (succes)
            {
                await _dialogService.ShowMessageBox("Registered", "Succesfully registered");
            }
            else
            {
                await _dialogService.ShowMessageBox("Error", "Oops, an error has occured");

            }

            _navigationService.GoBack();
        }
    }
}
