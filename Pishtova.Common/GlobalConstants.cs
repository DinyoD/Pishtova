using System.Collections.Generic;

namespace Pishtova.Common
{
    public class GlobalConstants
    {
        public const string AdminRoleName = "Administrator";

        public const string TeacherRoleName = "Teacher";

        public const string StudentRoleName = "Student";

        public  ICollection<string> TownsName = new List<string> { "Plovdiv", "Varna", "Sofia"};
    }
}
