using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ReportDateCalculator;
using ReportDateCalculator.Entity;

namespace ReportDateCalculatorTest
{
    [TestClass]
    public class NextReportDateTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region "RecurrenceDay - Tests"
        // Starts on June 5, 2022, repeats every 3 days, ends on July 2, 2022.
        [TestMethod]
        [TestCategory("RecurrenceDay")]
        public void GetReportDatesForDay_By_PastDate()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 5);
            schedularData.RepeatEvery = 3;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 7, 2);
            List<DateTime> actual = new NextReportDate().GetReportDatesForDay(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 5));
            expected.Add(new DateTime(2022, 6, 8));
            expected.Add(new DateTime(2022, 6, 11));
            expected.Add(new DateTime(2022, 6, 14));
            expected.Add(new DateTime(2022, 6, 17));
            expected.Add(new DateTime(2022, 6, 20));
            expected.Add(new DateTime(2022, 6, 23));
            expected.Add(new DateTime(2022, 6, 26));
            expected.Add(new DateTime(2022, 6, 29));
            expected.Add(new DateTime(2022, 7, 2));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 25, 2022, repeats every 3 days, ends on July 2, 2022.
        [TestMethod]
        [TestCategory("RecurrenceDay")]
        public void GetReportDatesForDay_By_FutureDate()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 25);
            schedularData.RepeatEvery = 3;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 7, 2);
            List<DateTime> actual = new NextReportDate().GetReportDatesForDay(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 25));
            expected.Add(new DateTime(2022, 6, 28));
            expected.Add(new DateTime(2022, 7, 1));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 25, 2022, repeats every 4 days, ends on July 30, 2022.
        [TestMethod]
        [TestCategory("RecurrenceDay")]
        public void GetReportDatesForDay_By_Repeat()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 25);
            schedularData.RepeatEvery = 4;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 7, 30);
            List<DateTime> actual = new NextReportDate().GetReportDatesForDay(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 25));
            expected.Add(new DateTime(2022, 6, 29));
            expected.Add(new DateTime(2022, 7, 3));
            expected.Add(new DateTime(2022, 7, 7));
            expected.Add(new DateTime(2022, 7, 11));
            expected.Add(new DateTime(2022, 7, 15));
            expected.Add(new DateTime(2022, 7, 19));
            expected.Add(new DateTime(2022, 7, 23));
            expected.Add(new DateTime(2022, 7, 27));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 5, 2022, repeats every 3 days, ends after 4 occurrence.
        [TestMethod]
        [TestCategory("RecurrenceDay")]
        public void GetReportDatesForDay_By_Occurrence()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 5);
            schedularData.RepeatEvery = 3;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = true;
            schedularData.OccurrenceNumber = 4;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForDay(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 5));
            expected.Add(new DateTime(2022, 6, 8));
            expected.Add(new DateTime(2022, 6, 11));
            expected.Add(new DateTime(2022, 6, 14));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 25, 2022, repeats every 5 days, never ends. Asume that never ends limit is 10 occurrence.
        [TestMethod]
        [TestCategory("RecurrenceDay")]
        public void GetReportDatesForDay_By_NeverEnds()
        {
            DailySchedularData schedularData = new DailySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 25);
            schedularData.RepeatEvery = 5;
            schedularData.EndsNever = true;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForDay(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 25));
            expected.Add(new DateTime(2022, 6, 30));
            expected.Add(new DateTime(2022, 7, 5));
            expected.Add(new DateTime(2022, 7, 10));
            expected.Add(new DateTime(2022, 7, 15));
            expected.Add(new DateTime(2022, 7, 20));
            expected.Add(new DateTime(2022, 7, 25));
            expected.Add(new DateTime(2022, 7, 30));
            expected.Add(new DateTime(2022, 8, 4));
            expected.Add(new DateTime(2022, 8, 9));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }
        #endregion

        #region "ReccurenceWeek - Tests"
        // Starts on June 1, 2022, repeats every week on Monday and Friday, ends on June 30, 2022.
        [TestMethod]
        [TestCategory("ReccurenceWeek")]
        public void GetReportDatesForWeek_By_PastDate()
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
            List<DateTime> actual = new NextReportDate().GetReportDatesForWeek(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 3));
            expected.Add(new DateTime(2022, 6, 6));
            expected.Add(new DateTime(2022, 6, 10));
            expected.Add(new DateTime(2022, 6, 13));
            expected.Add(new DateTime(2022, 6, 17));
            expected.Add(new DateTime(2022, 6, 20));
            expected.Add(new DateTime(2022, 6, 24));
            expected.Add(new DateTime(2022, 6, 27));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 23, 2022, repeats every week on Sunday and Thursday, ends on June 30, 2022.
        [TestMethod]
        [TestCategory("ReccurenceWeek")]
        public void GetReportDatesForWeek_By_FutureDate()
        {
            List<WeekDay> WeekDays = new List<WeekDay>();
            WeekDays.Add(WeekDay.Sunday);
            WeekDays.Add(WeekDay.Thursday);

            WeeklySchedularData schedularData = new WeeklySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 23);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatOn = WeekDays;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 6, 30);
            List<DateTime> actual = new NextReportDate().GetReportDatesForWeek(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 23));
            expected.Add(new DateTime(2022, 6, 26));
            expected.Add(new DateTime(2022, 6, 30));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 23, 2022, repeats every 2 week on Friday, ends on July 30, 2022.
        [TestMethod]
        [TestCategory("ReccurenceWeek")]
        public void GetReportDatesForWeek_By_Repeat()
        {
            List<WeekDay> WeekDays = new List<WeekDay>();
            WeekDays.Add(WeekDay.Friday);

            WeeklySchedularData schedularData = new WeeklySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 23);
            schedularData.RepeatEvery = 2;
            schedularData.RepeatOn = WeekDays;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 7, 30);
            List<DateTime> actual = new NextReportDate().GetReportDatesForWeek(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 24));
            expected.Add(new DateTime(2022, 7, 8));
            expected.Add(new DateTime(2022, 7, 22));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 1, 2022, repeats every week on Monday and Friday, ends after 4 occurrence.
        [TestMethod]
        [TestCategory("ReccurenceWeek")]
        public void GetReportDatesForWeek_By_Occurrence()
        {
            List<WeekDay> WeekDays = new List<WeekDay>();
            WeekDays.Add(WeekDay.Monday);
            WeekDays.Add(WeekDay.Friday);

            WeeklySchedularData schedularData = new WeeklySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 1);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatOn = WeekDays;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = true;
            schedularData.OccurrenceNumber = 4;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForWeek(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 3));
            expected.Add(new DateTime(2022, 6, 6));
            expected.Add(new DateTime(2022, 6, 10));
            expected.Add(new DateTime(2022, 6, 13));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 1, 2022, repeats every week on Saturday and Tuesday, never ends. Asume that never ends limit is 10 occurrence.
        [TestMethod]
        [TestCategory("ReccurenceWeek")]
        public void GetReportDatesForWeek_By_NeverEnds()
        {
            List<WeekDay> WeekDays = new List<WeekDay>();
            WeekDays.Add(WeekDay.Saturday);
            WeekDays.Add(WeekDay.Tuesday);

            WeeklySchedularData schedularData = new WeeklySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 1);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatOn = WeekDays;
            schedularData.EndsNever = true;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForWeek(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 4));
            expected.Add(new DateTime(2022, 6, 7));
            expected.Add(new DateTime(2022, 6, 11));
            expected.Add(new DateTime(2022, 6, 14));
            expected.Add(new DateTime(2022, 6, 18));
            expected.Add(new DateTime(2022, 6, 21));
            expected.Add(new DateTime(2022, 6, 25));
            expected.Add(new DateTime(2022, 6, 28));
            expected.Add(new DateTime(2022, 7, 2));
            expected.Add(new DateTime(2022, 7, 5));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }
        #endregion

        #region "RecurrenceMonth - Tests"
        // Starts on June 14, 2022, repeats every 2 months on 14th, ends on December 30, 2022.
        [TestMethod]
        [TestCategory("RecurrenceMonth")]
        public void GetReportDatesForMonth_By_DayOfMonth()
        {
            MonthlySchedularData schedularData = new MonthlySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 14);
            schedularData.RepeatEvery = 2;
            schedularData.RepeatBy = RepeatType.DayOfMonth;
            schedularData.MonthDay = 14;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 12, 30);
            List<DateTime> actual = new NextReportDate().GetReportDatesForMonth(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 14));
            expected.Add(new DateTime(2022, 8, 14));
            expected.Add(new DateTime(2022, 10, 14));
            expected.Add(new DateTime(2022, 12, 14));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 3, 2022, repeats every 2 months on 1st Monday, ends on December 30, 2022.
        [TestMethod]
        [TestCategory("RecurrenceMonth")]
        public void GetReportDatesForMonth_By_DayOfWeek()
        {
            MonthlySchedularData schedularData = new MonthlySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 3);
            schedularData.RepeatEvery = 2;
            schedularData.RepeatBy = RepeatType.DayOfWeek;
            schedularData.WeekNumber = 1;
            schedularData.DayOn = WeekDay.Monday;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = true;
            schedularData.EndDate = new DateTime(2022, 12, 30);
            List<DateTime> actual = new NextReportDate().GetReportDatesForMonth(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 6));
            expected.Add(new DateTime(2022, 8, 1));
            expected.Add(new DateTime(2022, 10, 3));
            expected.Add(new DateTime(2022, 12, 5));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 23, 2022, repeats every month on 4th Sunday, ends after 5 occurrence.
        [TestMethod]
        [TestCategory("RecurrenceMonth")]
        public void GetReportDatesForMonth_By_Occurrence()
        {
            MonthlySchedularData schedularData = new MonthlySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 23);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatBy = RepeatType.DayOfWeek;
            schedularData.WeekNumber = 4;
            schedularData.DayOn = WeekDay.Sunday;
            schedularData.EndsNever = false;
            schedularData.EndsAfterOccurrence = true;
            schedularData.OccurrenceNumber = 5;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForMonth(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 26));
            expected.Add(new DateTime(2022, 7, 24));
            expected.Add(new DateTime(2022, 8, 28));
            expected.Add(new DateTime(2022, 9, 25));
            expected.Add(new DateTime(2022, 10, 23));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }

        // Starts on June 23, 2022, repeats every month on 4th Sunday, never ends. Asume that never ends limit is 10 occurrence.
        [TestMethod]
        [TestCategory("RecurrenceMonth")]
        public void GetReportDatesForMonth_By_NeverEnds()
        {
            MonthlySchedularData schedularData = new MonthlySchedularData();
            schedularData.StartDate = new DateTime(2022, 6, 23);
            schedularData.RepeatEvery = 1;
            schedularData.RepeatBy = RepeatType.DayOfWeek;
            schedularData.WeekNumber = 4;
            schedularData.DayOn = WeekDay.Sunday;
            schedularData.EndsNever = true;
            schedularData.EndsAfterOccurrence = false;
            schedularData.OccurrenceNumber = 0;
            schedularData.EndsOn = false;
            schedularData.EndDate = new DateTime();
            List<DateTime> actual = new NextReportDate().GetReportDatesForMonth(schedularData);

            List<DateTime> expected = new List<DateTime>();
            expected.Add(new DateTime(2022, 6, 26));
            expected.Add(new DateTime(2022, 7, 24));
            expected.Add(new DateTime(2022, 8, 28));
            expected.Add(new DateTime(2022, 9, 25));
            expected.Add(new DateTime(2022, 10, 23));
            expected.Add(new DateTime(2022, 11, 27));
            expected.Add(new DateTime(2022, 12, 25));
            expected.Add(new DateTime(2023, 1, 22));
            expected.Add(new DateTime(2023, 2, 26));
            expected.Add(new DateTime(2023, 3, 26));

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                TestContext.WriteLine(actual[i].ToString("MM/dd/yyyy"));
                Assert.AreEqual(expected[i].ToString("MM/dd/yyyy"), actual[i].ToString("MM/dd/yyyy"));
            }
        }
        #endregion
    }
}