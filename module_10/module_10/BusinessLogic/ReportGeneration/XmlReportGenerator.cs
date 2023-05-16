using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLogic
{
    internal class XmlReportGenerator : IReportGenerator
    {
        public string GenerateAttendanceReport(object reportData)
        {
            XmlSerializer serializer = new(reportData.GetType());
            var xmlData = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, reportData);
                    xmlData = sww.ToString();
                }
            }

            return xmlData;
        }
    }
}