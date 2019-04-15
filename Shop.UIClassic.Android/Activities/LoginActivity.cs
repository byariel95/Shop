﻿

namespace Shop.UIClassic.Android.Activities
{
    using System;
    using global::Android.App;
    using global::Android.OS;
    using global::Android.Support.V7.App;
    using global::Android.Widget;
    using Common.Services;
    using Helpers;
    using global::Android.Views;
    using Common.Models;

    [Activity(Label = "@string/login", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity
    {
        private ApiService apiService;
        private EditText emailText;
        private EditText passwordText;
        private Button loginButton;
        private ProgressBar activityIndicatorProgressBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.LoginPage);
            this.FindViews();
            this.HandleEvents();
            this.SetInitialData();

        }

        private void SetInitialData()
        {
            this.apiService = new ApiService();
            this.emailText.Text = "jzuluaga55@gmail.com";
            this.passwordText.Text = "123456";
            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
        }

        private void HandleEvents()
        {
            this.loginButton.Click += this.LoginButton_Click;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.emailText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.passwordText.Text))
            {
                DiaglogService.ShowMessage(this, "Error", "You must enter a password.", "Accept");
                return;
            }

            this.activityIndicatorProgressBar.Visibility = ViewStates.Visible;
            this.loginButton.Enabled = false;

            var request = new TokenRequest
            {
                Password = this.passwordText.Text,
                Username = this.emailText.Text
            };

            var response = await this.apiService.GetTokenAsync(
                "https://shopzulu.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            this.activityIndicatorProgressBar.Visibility = ViewStates.Invisible;
            this.loginButton.Enabled = true;
            if (!response.IsSuccess)
            {
                DiaglogService.ShowMessage(this, "Error", "User or password incorrect.", "Accept");
                return;
            }

            DiaglogService.ShowMessage(this, "Ok", "Fuck Yeah!", "Accept");
        }

        private void FindViews()
        {
            this.emailText = this.FindViewById<EditText>(Resource.Id.emailText);
            this.passwordText = this.FindViewById<EditText>(Resource.Id.passwordText);
            this.loginButton = this.FindViewById<Button>(Resource.Id.loginButton);
            this.activityIndicatorProgressBar = this.FindViewById<ProgressBar>(Resource.Id.activityIndicatorProgressBar);
        }

    }
   

}
