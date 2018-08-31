using System;
using System.Collections.Generic;
using Xamarin.Forms;  
using Xamarin.Forms.Xaml;  
using RightPath.Models.ViewModel;  

namespace RightPath.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            var vm = new SigupViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Signup, try again", "OK");
            vm.PushMainPage += () => Navigation.PushAsync(new MainPage("Start"));
            vm.PushEulaPage += () => Navigation.PushAsync(new EULAPage());

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Name.Completed += (object sender, EventArgs e) =>
            {
                Email.Focus();
            };

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };

        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

    }
}
