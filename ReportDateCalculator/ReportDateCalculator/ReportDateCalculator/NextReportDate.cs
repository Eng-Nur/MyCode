using System;
using System.Collections.Generic;
using System.Globalization;
using ReportDateCalculator.Entity;

namespace ReportDateCalculator
{
    public class NextReportDate
    {
        private const int EndsNeverLimit = 10;

        public List<DateTime> GetReportDatesForDay(DailySchedularData data)
        {
            List<DateTime> nexttDates = new List<DateTime>();
            int occurrence = 0;
            DateTime beginDate = data.StartDate > DateTime.Now ? data.StartDate : DateTime.Now;
            DateTime endDate = data.EndDate;

            /*
            // Limit the search within current month
            endDate = GetEndDateOfCurrentMonth(beginDate);
            if (data.EndDate.Date < endDate.Date)
                endDate = data.EndDate;
            */

            DateTime date = data.StartDate;

            // If StartDate is a past date
            while (date.Date < beginDate.Date)
                date = date.AddDays(data.RepeatEvery);

            if (data.EndsOn) // Ends on a specific date
            {
                while (date <= endDate)
                {
                    nexttDates.Add(date);
                    date = date.AddDays(data.RepeatEvery);
                }
            }
            else // Never ends or ends after occurrence
            {
                occurrence = data.EndsAfterOccurrence ? data.OccurrenceNumber : EndsNeverLimit;

                for (int i = 0; i < occurrence; i++)
                {
                    nexttDates.Add(date);
                    date = date.AddDays(data.RepeatEvery);
                }
            }

            return nexttDates;
        }

        public List<DateTime> GetReportDatesForWeek(WeeklySchedularData data)
        {
            List<DateTime> nexttDates = new List<DateTime>();
            int occurrence = 0;
            DateTime beginDate = data.StartDate > DateTime.Now ? data.StartDate : DateTime.Now;
            DateTime endDate = data.EndDate;

            /*
            // Limit the search within current month
            endDate = GetEndDateOfCurrentMonth(beginDate);
            if (data.EndDate.Date < endDate.Date)
                endDate = data.EndDate;
            */

            DateTime date = data.StartDate;

            // If StartDate is a past date
            while (GetWeekOfYear(date) < GetWeekOfYear(beginDate))
                date = date.AddDays(data.RepeatEvery * 7);

            date = GetFirstDateOfWeek(date, CultureInfo.CurrentCulture);
            if (data.EndsOn) // Ends on a specific date
            {
                while (date <= endDate)
                {
                    for (int i = 0; i < data.RepeatOn.Count; i++)
                    {
                        date = GetNextDateFromWeekDay(date, data.RepeatOn[i]);
                        if (date >= beginDate && date <= endDate)
                            nexttDates.Add(date);
                    }
                    date = date.AddDays(((data.RepeatEvery - 1) * 7) + 1);
                }
            }
            else // Never ends or ends after occurrence
            {
                occurrence = data.EndsAfterOccurrence ? data.OccurrenceNumber : EndsNeverLimit;

                while (nexttDates.Count < occurrence)
                {
                    for (int i = 0; i < data.RepeatOn.Count; i++)
                    {
                        date = GetNextDateFromWeekDay(date, data.RepeatOn[i]);
                        if (date >= beginDate && nexttDates.Count < occurrence)
                            nexttDates.Add(date);
                    }
                    date = date.AddDays((data.RepeatEvery - 1) * 7);
                }
            }

            return nexttDates;
        }

        public List<DateTime> GetReportDatesForMonth(MonthlySchedularData data)
        {
            List<DateTime> nexttDates = new List<DateTime>();
            int occurrence = 0;
            DateTime beginDate = data.StartDate > DateTime.Now ? data.StartDate : DateTime.Now;
            DateTime endDate = data.EndDate;

            /*
            // Limit the search within current month
            endDate = GetEndDateOfCurrentMonth(beginDate);
            if (data.EndDate.Date < endDate.Date)
                endDate = data.EndDate;
            */

            DateTime date = data.StartDate;

            // If StartDate is a past date
            while (date.Date.Month < beginDate.Date.Month)
                date = date.AddMonths(data.RepeatEvery);

            DateTime newDate = new DateTime();
            if (data.EndsOn) // Ends on a specific date
            {
                while (date <= endDate)
                {
                    if (data.RepeatBy == RepeatType.DayOfMonth)
                        newDate = new DateTime(date.Year, date.Month, data.MonthDay);
                    else if (data.RepeatBy == RepeatType.DayOfWeek)
                        newDate = GetDateByWeek(date, data.WeekNumber, data.DayOn);

                    if (newDate >= beginDate && newDate <= endDate)
                        nexttDates.Add(newDate);

                    date = date.AddMonths(data.RepeatEvery);
                }
            }
            else // Never ends or ends after occurrence
            {
                occurrence = data.EndsAfterOccurrence ? data.OccurrenceNumber : EndsNeverLimit;

                while (nexttDates.Count < occurrence)
                {
                    if (data.RepeatBy == RepeatType.DayOfMonth)
                        newDate = new DateTime(date.Year, date.Month, data.MonthDay);
                    else if (data.RepeatBy == RepeatType.DayOfWeek)
                        newDate = GetDateByWeek(date, data.WeekNumber, data.DayOn);

                    if (newDate >= beginDate)
                        nexttDates.Add(newDate);

                    date = date.AddMonths(data.RepeatEvery);
                }
            }

            return nexttDates;
        }

        #region "Helper Methods"
        private DateTime GetDateByWeek(DateTime startDate, int weekNumber, WeekDay weekDay)
        {
            DateTime date = new DateTime(startDate.Year, startDate.Month, 1);
            DateTime endOfMonth = GetEndDateOfCurrentMonth(startDate);

            date = date.AddDays((weekNumber - 1) * 7);

            //if (date.DayOfWeek.ToString() == weekDay.ToString())
            //    date = date.AddDays(1);

            for (int i = 1; i <= 7; i++)
            {
                if (date <= endOfMonth)
                {
                    if (date.DayOfWeek.ToString() == weekDay.ToString())
                        return date;
                }
                date = date.AddDays(1);
            }

            return date;
        }

        //private int GetWeekNumberInMonth(DateTime date)
        //{
        //    // KB: Days of week starts by default as Sunday = 0

        //    DateTime firstOfMonth = new DateTime(date.Year, date.Month, 1);
        //    int firstDayOfMonth = (int)firstOfMonth.DayOfWeek;
        //    return (int)Math.Ceiling((firstDayOfMonth + DateTime.DaysInMonth(date.Year, date.Month)) / 7.0);
        //}

        private DateTime GetEndDateOfCurrentMonth(DateTime beginDate)
        {
            DateTime endDate = beginDate;
            int month = beginDate.Month;

            while (month == beginDate.Month)
            {
                endDate = endDate.AddDays(1);
                month = endDate.Month;
            }
            endDate = endDate.AddDays(-1);

            return endDate;
        }

        private int GetWeekOfYear(DateTime date)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                date = date.AddDays(3);

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private DateTime GetFirstDateOfWeek(DateTime date, CultureInfo cultureInfo)
        {
            DateTime firstDateOfWeek = date.Date;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            while (firstDateOfWeek.DayOfWeek != firstDayOfWeek)
                firstDateOfWeek = firstDateOfWeek.AddDays(-1);

            return firstDateOfWeek;
        }

        private DateTime GetNextDateFromWeekDay(DateTime date, WeekDay weekDay)
        {
            for (int i = 1; i <= 7; i++)
            {
                if (date.DayOfWeek.ToString() == weekDay.ToString())
                    return date;
                date = date.AddDays(1);
            }

            return date;
        }
        #endregion
    }
}