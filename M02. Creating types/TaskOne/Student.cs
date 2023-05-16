namespace Students
{
    internal class Student
    {
        private string FullName { get; set; }
        private string Email { get; set; }
        private const string domain = "@epam.com";

        public Student(string email)
        {
            var fullName = string.Empty;
            string[] arr = email.Split("@");
            string[] lowerName = arr[0].Split(".");
            lowerName[0] = lowerName[0] + " ";

            for (int i = 0; i < lowerName.Length; i++)
            {
                char[] letters = lowerName[i].ToCharArray();
                letters[0] = char.ToUpper(letters[0]);
                fullName += new string(letters);
            }

            FullName = fullName;
            Email = email;
        }

        public Student(string name, string surname)
        {
            FullName = name + " " + surname;
            Email = name.ToLower() + "." + surname.ToLower() + domain;

        }

        public override bool Equals(object obj)
        {
            if (obj is Student otherStudent)
            {
                return (FullName == otherStudent.FullName) && (Email == otherStudent.Email);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return FullName.Length * Email.Length * (int)FullName[0] * (int)FullName[FullName.Length - 1];
        }


    }
}
