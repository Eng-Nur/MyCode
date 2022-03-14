using System;
using System.Collections.Generic;

namespace ReportDateCalculator
{
    public interface IRecurrence
    {
        object SchedularData { get; set; }

        List<DateTime> GetNextReportDate();
        string ToXML();
        object FromXML(string xml);
    }
}