using System;
using System.Collections.Generic;
using ReportDateCalculator.Entity;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace ReportDateCalculator
{
    public class ReccurenceWeek : IRecurrence
    {
        private object schedularData;

        public object SchedularData
        {
            get { return schedularData; }
            set { schedularData = value; }
        }

        public List<DateTime> GetNextReportDate()
        {
            return new NextReportDate().GetReportDatesForWeek((WeeklySchedularData)SchedularData);
        }

        public string ToXML()
        {
            string xml = "";
            XmlSerializer serializer = new XmlSerializer(typeof(WeeklySchedularData));

            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                {
                    serializer.Serialize(xmlWriter, SchedularData);
                    xml = stringWriter.ToString();
                }
            }

            return xml;
        }

        public object FromXML(string xml)
        {
            object data;
            XmlSerializer serializer = new XmlSerializer(typeof(WeeklySchedularData));

            using (TextReader reader = new StringReader(xml))
            {
                data = serializer.Deserialize(reader);
            }

            return data;
        }
    }
}