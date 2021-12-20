namespace ProjectHelper
{
    using Sandbox;
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            var fbHelper = new FirebaseHelper(SandBoxConstants.AuthSecret, SandBoxConstants.BasePath);

            var result = fbHelper.GetSubjectInfo(@"test_bulgarian");

            Console.WriteLine();
        }

    }

}
