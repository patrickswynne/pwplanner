using System;
using System.ComponentModel;
using WGUPlanner.Models;
using Xamarin.Forms;

namespace WGUPlanner.Views
{
    [DesignTimeVisible(false)]
    public partial class NewTermPage : ContentPage
    {
        private Planner viewModel;
        public NewTermPage(Planner term)
        {
            InitializeComponent();

            BindingContext = this.viewModel = term;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await App.PlannerRepository.AddNewTermAsync(this.viewModel);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}