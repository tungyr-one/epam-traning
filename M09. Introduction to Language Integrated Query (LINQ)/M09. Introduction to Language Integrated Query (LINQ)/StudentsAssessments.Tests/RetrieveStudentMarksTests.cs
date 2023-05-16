using NUnit.Framework;
using System.Collections.Generic;
using StudentsAssesments;
using System.Text.Json;
using System.IO;
using System;
using System.Linq;

namespace StudentsAssessments.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            List<StudentAssessment> studentsTest = new();
            List<StudentAssessment> _data = new List<StudentAssessment>();
            _data.Add(new("Ivan Petrov", "Maths", "25/11/2012", 4));
            _data.Add(new("Ivan Ivanov", "Maths", "30/11/2012", 3));
            _data.Add(new("Vasya Pupkin", "History", "19/12/2012", 5));

            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(AppContext.BaseDirectory + "\\studentsTest.json", json);
        }

        [TestCase("-minmark 5", "- Vasya Pupkin History 19.12.2012 5\n")]
        [TestCase("-dateto 25/11/2012", "- Ivan Petrov Maths 25.11.2012 4\n")]
        [TestCase("-name Ivan - minmark 3 - maxmark 5 - datefrom 20 / 11 / 2012 - dateto 20 / 12 / 2012 - test Maths -sort name asc", 
                    "- Ivan Ivanov Maths 30.11.2012 3\n- Ivan Petrov Maths 25.11.2012 4\n")]
        public void Retrieve_Student_Marks(string testOptions, string expectedResult)
        {
            // act
            var loadedStudentsMarks = Program.LoadJSON(AppContext.BaseDirectory + "\\studentsTest.json");
            CMDParser testParser = new();
            var parsedOptions = testParser.ParseOptions(testOptions);
            StudentMarksRetriever testRetriever = new();
            var studentsMarksCollection = testRetriever.RetrieveStudentMarks(loadedStudentsMarks, parsedOptions);
            var actual = testRetriever.PrintQuery(studentsMarksCollection);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Retrieve_Student_Marks_Sort()
        {
            //arrange
            var testSort = "-sort mark desc";
            var expectedResult = "Vasya PupkinIvan Ivanov";

            // act
            var loadedStudentsMarks = Program.LoadJSON(AppContext.BaseDirectory + "\\studentsTest.json");
            CMDParser testParser = new();
            var parsedOptions = testParser.ParseOptions(testSort);
            StudentMarksRetriever testRetriever = new();
            var studentsMarksCollection = testRetriever.RetrieveStudentMarks(loadedStudentsMarks, parsedOptions).ToList();
            var actual = studentsMarksCollection[0].Name + studentsMarksCollection[studentsMarksCollection.Count - 1].Name;

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }
    }
}