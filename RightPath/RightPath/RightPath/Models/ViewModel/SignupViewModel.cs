using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using RightPath.Models;
using RightPath.Models.ResponseModel;

namespace RightPath.Models.ViewModel
{
    public class SigupViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public Action PushMainPage;
        public Action PushEulaPage;


        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public ICommand SubmitCommand { protected set; get; }
        public SigupViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        async void OnSubmit()
        {
            User user = new User(name,email, password);
            LoginResponse loginResponse = await App.userManager.SignupTaskAsync(user);
            if (loginResponse.success == true)
            {
                Application.Current.Properties["isLoggedIn"] = true;
                object eulaAccepted;
                var hasKey = Application.Current.Properties.TryGetValue("eulaAccepted", out eulaAccepted);
                if (!hasKey || !(bool)eulaAccepted)
                {
                    PushEulaPage();
                }
                else
                {
                    PushMainPage();
                }
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
        }
    }
}
