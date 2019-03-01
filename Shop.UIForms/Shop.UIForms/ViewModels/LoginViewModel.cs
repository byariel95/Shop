



namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Shop.UIForms.Views;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            Email = "byariel";
            Password = "123";
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "You must enter an Email", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "You must enter an Password", "Aceptar");
                return;
            }
            if (!this.Email.Equals("byariel") || !this.Password.Equals("123"))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Your Password or User are incorrect", "Aceptar");
                return;
            }
            // await Application.Current.MainPage.DisplayAlert("Yeah!", "Amazing!!", "ok");
            MainViewModel.GetInstance().Products = new ProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

        }
    }
}
