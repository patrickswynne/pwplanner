using System;
using System.ComponentModel;
using WGUPlanner.Models;
using Xamarin.Forms;

namespace WGUPlanner.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TermEditPage : ContentPage
    {
        private Planner viewModel;

        public TermEditPage(Planner term)
        {
            InitializeComponent();

            BindingContext = this.viewModel = term;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            await App.PlannerRepository.UpdateTermAsync(this.viewModel);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            await App.PlannerRepository.DeleteTermAsync(this.viewModel.TermTitle);
            await Navigation.PopModalAsync();
        }
    }
}