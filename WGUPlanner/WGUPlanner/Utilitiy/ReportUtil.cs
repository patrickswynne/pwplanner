using System;
using System.Collections.Generic;
using System.Text;
using WGUPlanner.Models;
using Xamarin.Forms;

namespace WGUPlanner.Util
{
    public static class ReportUtil
    {
        public static Report getReport(int statistic, string reportName)
        {
            return new Report(statistic, reportName);
        }

        public static StackLayout BuildReport(List<Report> reportList)
        {
            StackLayout rootStack = new StackLayout();
            StackLayout main = new StackLayout();
            foreach (var r in reportList)
            {
                StackLayout row = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(5)
                };
                StackLayout cellLeft = new StackLayout();
                cellLeft.Children.Add(
                        new Label { 
                            Text = r.getstatistic().ToString(), 
                            FontSize = (double)20, 
                            FontAttributes = FontAttributes.Bold, 
                            HorizontalOptions = LayoutOptions.Start 
                        });
                StackLayout cellRight = new StackLayout();
                cellRight.Children.Add(
                    new Label { 
                        Text = r.getreportTitle(), 
                        FontSize = (double)14,
                        FontAttributes = FontAttributes.Bold, 
                        HorizontalOptions = LayoutOptions.End 
                    });
                row.Children.Add(cellLeft);
                row.Children.Add(cellRight);

                main.Children.Add(row);
            }

            rootStack.Children.Add(main);
            rootStack.Children.Add(new Label
            {
                Text = $"as of {DateTime.Now}",
                FontSize = (double)10,
                Margin = new Thickness(5),
                FontAttributes = FontAttributes.Italic
            });
            return rootStack;
        }
    }
}
