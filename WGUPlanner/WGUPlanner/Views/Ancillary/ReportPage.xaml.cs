using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGUPlanner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGUPlanner.Util;

namespace WGUPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {

        public ReportPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }


        async void LoadData()
        {
            try
            {
                root.Content = ReportUtil.BuildReport(await DataAccess.GetReportData());
            }
            catch(NullReferenceException nullReferenceException)
            {
                Console.WriteLine(nullReferenceException);
            }
        }
    }
}