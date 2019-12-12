using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUPlanner.Implementation;
using WGUPlanner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        BusinessImplementation bi = new BusinessImplementation();
        public Registration()
        {
            InitializeComponent();

            InitlializeFormAsync().Wait();
        }

        //
        // HERE WE ARE CREATING THE REGISTRATION FORM
        //
        async Task InitlializeFormAsync()
        {
            StackLayout loginStack = new StackLayout();
            loginStack.Children.Add(new Label { Text = "Registration", FontSize = (double)14, Margin = new Thickness(5) });
            //Email
            loginStack.Children.Add(new Label { Text = "Email", FontSize = (double)14, Margin = new Thickness(5) });
            Editor email = new Editor { Placeholder = "Email", FontSize = (double)14, Margin = new Thickness(5) };
            loginStack.Children.Add(email);
            //Password
            loginStack.Children.Add(new Label { Text = "Password", FontSize = (double)14, Margin = new Thickness(5) });
            Editor password = new Editor { Placeholder = "Password", FontSize = (double)14, Margin = new Thickness(5) };
            loginStack.Children.Add(password);
            Button loginButton = new Button { Text = "Login", FontSize = (double)14, Margin = new Thickness(5) };
            loginButton.Clicked += async delegate
            {
                Users user = new Users();
                user.Email = email.Text;
                user.Password = password.Text;
                if (bi.validUser(user))
                {
                    await App.LoginRepository.AddNewUserAsync(user);
                    GoToMain();
                }                
            };
            loginStack.Children.Add(loginButton);
            //if the user does exist show the login form
            root.Content = loginStack;
        }

        public async void GoToMain()
        {
            await Navigation.PushModalAsync(new MainPage());
        }
    }
}