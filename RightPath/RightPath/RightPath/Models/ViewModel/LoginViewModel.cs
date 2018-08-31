using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using RightPath.Models;
using RightPath.Models.ResponseModel;


namespace RightPath.Models.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public Action PushMainPage;
        public Action PushEulaPage;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
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
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        async void OnSubmit()
        {
            User user = new User(email, password);
            LoginResponse loginResponse = await App.userManager.LoginTaskAsync(user);
            if (loginResponse.success == true)
            {
                Application.Current.Properties["isLoggedIn"] = true;

                object eulaAccepted;
                var hasKey = Application.Current.Properties.TryGetValue("eulaAccepted", out eulaAccepted);
                if (!hasKey || !(bool)eulaAccepted)
                {
                    PushEulaPage();                    
                }else{
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
