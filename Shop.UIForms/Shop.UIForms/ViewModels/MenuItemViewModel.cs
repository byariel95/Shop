﻿

namespace Shop.UIForms.ViewModels
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Shop.Common.Helpers;
    using Views;
    using Xamarin.Forms;

    public class MenuItemViewModel : Common.Models.Menu
    {

        

        public ICommand SelectMenuCommand => new RelayCommand(this.SelectMenu);

        private async void SelectMenu()
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "ProfilePage":
                    mainViewModel.Profile = new ProfileViewModel();
                    await App.Navigator.PushAsync(new ProfilePage());
                    break;
                default:
                    Settings.User = string.Empty;
                    Settings.IsRemember = false;
                    Settings.Token = string.Empty;
                    Settings.UserEmail = string.Empty;
                    Settings.UserPassword = string.Empty;

                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }

}
