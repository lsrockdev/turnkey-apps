using System;
using System.Collections.Generic;
using Xamarin.Forms;  
using Xamarin.Forms.Xaml;  
using RightPath.Models;  

namespace RightPath.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();

        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();

        }

    }
}
