using System;
using System.Collections.Generic;

namespace ReportDateCalculator.Entity
{
    public class WeeklySchedularData
    {
        public DateTime StartDate { get; set; }

        public int RepeatEvery { get; set; }

        public List<WeekDay> RepeatOn { get; set; }

        public bool EndsNever { get; set; }

        public bool EndsAfterOccurrence { get; set; }

        public int OccurrenceNumber { get; set; }

        public bool EndsOn { get; set; }

        public DateTime EndDate { get; set; }
    }
}