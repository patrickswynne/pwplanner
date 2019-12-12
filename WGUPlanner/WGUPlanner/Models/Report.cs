using System;
using System.Collections.Generic;
using System.Text;

namespace WGUPlanner.Models
{
    public class Report
    {
       
        // do not export private members
        private int statistic;
        private String reportTitle;

        public Report(int statistic, String reportTitle)
        {
            this.statistic = statistic;
            this.reportTitle = reportTitle;
        }

        public int getstatistic()
        {
            return this.statistic;
        }

        public String getreportTitle()
        {
            return this.reportTitle;
        }
    }
}
