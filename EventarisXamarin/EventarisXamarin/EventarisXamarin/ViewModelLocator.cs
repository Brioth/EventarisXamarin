using EventarisXamarin.Services;
using EventarisXamarin.ViewModel;
using EventarisXDal;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace EventarisXamarin
{
    public class ViewModelLocator
    {
        public const string EditProfilePageKey = "EditProfilePage";
        public const string NewProfilePageKey = "NewProfilePage";
        public const string CustomScanPageKey = "CustomScanPage";

        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NewProfileViewModel>();
            SimpleIoc.Default.Register<EditProfileViewModel>();
            SimpleIoc.Default.Register<CustomScanViewModel>();
        }

        public MainViewModel Main
        {
            get { return SimpleIoc.Default.GetInstance<MainViewModel>(); }
        }

        public EditProfileViewModel Edit
        {
            get { return SimpleIoc.Default.GetInstance<EditProfileViewModel>(); }
        }

        public NewProfileViewModel New
        {
            get { return SimpleIoc.Default.GetInstance<NewProfileViewModel>(); }
        }

        public CustomScanViewModel Scan
        {
            get { return SimpleIoc.Default.GetInstance<CustomScanViewModel>(); }
        }

    }
}
