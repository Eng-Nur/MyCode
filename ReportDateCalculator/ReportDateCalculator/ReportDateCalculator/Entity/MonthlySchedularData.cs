using System;

namespace ReportDateCalculator.Entity
{
    public class MonthlySchedularData
    {
        public DateTime StartDate { get; set; }

        public int RepeatEvery { get; set; }

        public RepeatType RepeatBy { get; set; }

        public int MonthDay { get; set; }

        public int WeekNumber { get; set; }

        public WeekDay DayOn { get; set; }

        public bool EndsNever { get; set; }

        public bool EndsAfterOccurrence { get; set; }

        public int OccurrenceNumber { get; set; }

        public bool EndsOn { get; set; }

        public DateTime EndDate { get; set; }
    }
}