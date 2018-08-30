using System;
using System.Collections.Generic;
using Xamarin.Forms;  
using Xamarin.Forms.Xaml;  
using RightPath.Models;  

namespace RightPath.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            vm.PushMainPage += () => Navigation.PushAsync(new MainPage("Start"));

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }

        private void Signup_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignupPage());
        }
    }
}
