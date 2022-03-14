using System;

namespace ReportDateCalculator.Entity
{
    public class DailySchedularData
    {
        public DateTime StartDate { get; set; }

        public int RepeatEvery { get; set; }

        public bool EndsNever { get; set; }

        public bool EndsAfterOccurrence { get; set; }

        public int OccurrenceNumber { get; set; }

        public bool EndsOn { get; set; }

        public DateTime EndDate { get; set; }
    }
}