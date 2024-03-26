using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWorkTrace.Utility
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_Worker = "Worker";
    }
    public static class Title
    {
        public const string Title_Software_Architect = "Software Architect";
        public const string Title_Analyst = "Analyst";
        public const string Title_Software_Intern = "Software Intern";
        public const string Title_Software_Developer = "Software Developer";
        public const string Title_Analysis_Specialist = "Analysis Specialist";
        public const string Title_Service_Manager = "Service Manager";
        private static readonly List<string> _titles = new List<string>
    {
        "Software Architect",
        "Analyst",
        "Software Intern",
        "Software Developer",
        "Analysis Specialist",
        "Service Manager"
    };

        public static IEnumerable<string> GetTitles()
        {
            return _titles;
        }
    }
}
