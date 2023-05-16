using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudentsAssesments
{
    public class Program
    {
        static void Main(string[] args)
        {
            var deserializedListStudents = LoadJSON(AppContext.BaseDirectory + "\\students.json");            

            Dictionary<string, string> userOptions = new();

            while (true)
            {
                userOptions.Clear();
                Console.WriteLine("Hello! Please input your criteria: ");
                var userInput = Console.ReadLine();
                
                CMDParser parser = new();
                userOptions = parser.ParseOptions(userInput);
                if(parser.Interrupt)
                {
                    continue;
                }

                StudentMarksRetriever marks = new();
                var retrievedMarks = marks.RetrieveStudentMarks(deserializedListStudents, userOptions);
                marks.PrintQuery(retrievedMarks);
            }            
        }

        public static IEnumerable<StudentAssessment> LoadJSON(string filepath)
        {
            var fileContent = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<IEnumerable<StudentAssessment>>(fileContent);
        }
    }
}
