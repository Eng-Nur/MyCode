using System;
using System.Collections.Generic;
using ReportDateCalculator.Entity;

namespace ReportDateCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<DateTime> dateTimes;

            RecurrenceDay recurrenceDay = new RecurrenceDay();
            recurrenceDay.SchedularData = GetDailySchedularData();
            //Console.WriteLine(recurrenceDay.ToXML());
            dateTimes = recurrenceDay.GetNextReportDate();
            for (int i = 0; i < dateTimes.Count; i++)
                Console.WriteLine(dateTimes[i].ToString("MM/dd/yyyy"));
            
            Console.WriteLine("---------------------");

            ReccurenceWeek reccurenceWeek = new ReccurenceWeek();
            reccurenceWeek.SchedularData = GetWeeklySchedularData();
            //Console.WriteLine(reccurenceWeek.ToXML());
            dateTimes = reccurenceWeek.GetNextReportDate();
            for (int i = 0; i < dateTimes.Count; i++)
                Console.WriteLine(dateTimes[i].ToString("MM/dd/yyyy"));

            Console.WriteLine("---------------------");

            RecurrenceMonth recurrenceMonth = new RecurrenceMonth();
            recurrenceMonth.SchedularData = GetMonthlySchedularData();
            //Console.WriteLine(recurrenceMonth.ToXML());
            dateTimes = recurrenceMonth.GetNextReportDate();
            for (int i = 0; i < dateTimes.Count; i++)
                Console.WriteLine(dateTimes[i].ToString("MM/dd/yyyy"));

            Console.ReadLine();
        }

        #region "Get Input Data"
        private static DailySchedularData GetDailySchedularData()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 5);
            schedularData.RepeatEvery = 3;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 7, 2);

            return schedularData;
        }

        private static WeeklySchedularData GetWeeklySchedularData()
        {
            List<WeekDay> WeekDays = new List<WeekDay>();
            WeekDays.Add(WeekDay.Monday);
            WeekDays.Add(WeekDay.Friday);

            WeeklySchedularData schedularData = new WeeklySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 1);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatOn = WeekDays;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 6, 30);

            return schedularData;
        }

        private static MonthlySchedularData GetMonthlySchedularData()
        {
            MonthlySchedularData schedularData = new MonthlySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 5);
            schedularData.RepeatEvery = 2;
            schedularData.RepeatBy = RepeatType.DayOfMonth;
            schedularData.MonthDay = 14;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 12, 30);

            return schedularData;
        }
        #endregion
    }
}