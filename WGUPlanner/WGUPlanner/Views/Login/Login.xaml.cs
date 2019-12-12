using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGUPlanner.Util;
using WGUPlanner.Models;
using System.Threading.Tasks;

namespace WGUPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            InitlializeForm();
        }

        //
        // HERE WE ARE CREATING THE LOGIN FORM
        //
        void InitlializeForm()
        {
            StackLayout loginStack = new StackLayout();
            loginStack.Children.Add(new Label { Text = "Welcome, please login", FontSize = (double)14, Margin = new Thickness(5) });
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
                //check if user exists, if not return to register other wise progress to mainPage.
                bool isUser = false;
                try
                {
                    var x = App.LoginRepository.GetUserLoginAsync(user);
                }
                catch (NullReferenceException nullReferenceException)
                {
                    // we didn't find a user, continue on to the registration screen
                    Console.WriteLine(nullReferenceException.ToString());
                }
                catch (AggregateException aggregateException)
                {
                    // we didn't find a user, continue on to the registration screen
                    Console.WriteLine(aggregateException.ToString());
                }
                if (isUser)
                {
                    GoToMain();
                }
                else
                {
                    GoToLogin();
                }
            };

            loginStack.Children.Add(loginButton);
            root.Content = loginStack;
        }
        public async void GoToMain()
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        public async void GoToLogin()
        {
            await Navigation.PushModalAsync(new Login());
        }
    }
}