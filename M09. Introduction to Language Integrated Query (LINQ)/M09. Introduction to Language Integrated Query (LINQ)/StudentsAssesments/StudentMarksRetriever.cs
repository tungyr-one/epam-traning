using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsAssesments
{
    public class StudentMarksRetriever
    {
        public IEnumerable<StudentAssessment> RetrieveStudentMarks(IEnumerable<StudentAssessment> deserializedListStudents, Dictionary<string, string> userOptions)
        {
            IEnumerable<StudentAssessment> query = deserializedListStudents;

            if (userOptions.ContainsKey("name"))
            {
                query = query.Where(stud => stud.Name.Contains(userOptions["name"]));
            }

            if (userOptions.ContainsKey("minmark"))
            {
                query = query.Where(stud => stud.Mark >= Convert.ToInt32(userOptions["minmark"]));
            }

            if (userOptions.ContainsKey("maxmark"))
            {
                query = query.Where(stud => stud.Mark <= Convert.ToInt32(userOptions["maxmark"]));
            }

            if (userOptions.ContainsKey("datefrom"))
            {
                query = query.Where(stud => stud.Date >= Convert.ToDateTime(userOptions["datefrom"]));
            }

            if (userOptions.ContainsKey("dateto"))
            {
                query = query.Where(stud => stud.Date <= Convert.ToDateTime(userOptions["dateto"]));
            }

            if (userOptions.ContainsKey("test"))
            {
                query = query.Where(stud => stud.Test == userOptions["test"]);
            }

            if (userOptions.ContainsKey("sort"))
            {
                string[] sortParams = new string[2];

                sortParams[0] = userOptions["sort"];
                sortParams[1] = userOptions["order"];

                switch ((sortParams[0], sortParams[1]))
                {
                    case ("name", "asc"):
                        query = query.OrderBy(x => x.Name);
                        break;

                    case ("mark", "asc"):
                        query = query.OrderBy(x => x.Mark);
                        break;

                    case ("date", "asc"):
                        query = query.OrderBy(x => x.Date);
                        break;

                    case ("test", "asc"):
                        query = query.OrderBy(x => x.Test);
                        break;

                    case ("name", "desc"):
                        query = query.OrderByDescending(x => x.Name);
                        break;

                    case ("mark", "desc"):
                        query = query.OrderByDescending(x => x.Mark);
                        break;

                    case ("date", "desc"):
                        query = query.OrderByDescending(x => x.Date);
                        break;

                    case ("test", "desc"):
                        query = query.OrderByDescending(x => x.Test);
                        break;
                }
            }

            return query;
        }

        public string PrintQuery(IEnumerable<StudentAssessment> query)
        {
            string result = string.Empty;
            Console.WriteLine((char)248 + " Student Test Date Mark:\n");
            foreach (var stud in query)
            {
                result += "- " + stud.Name + " " + stud.Test + " " + stud.Date.ToString("d") + " " + stud.Mark + "\n";
            }
            Console.WriteLine(result);
            return result;
        }
    }
}
