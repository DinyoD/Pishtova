using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.Test
{
    public class Pattern
    {
        public Dictionary<string, List<int>> ProblemsSubjectIDs = new()
        {
            //{ "bel", new List<int>{47,47,47,47,47,46,46,46,45,45,48,48,48,48,48,48,48,48,48,48} },
            //{ "bio", new List<int>{1,1,2,3,3,4,5,5,6,7,7,8,9,9,10,10,11,11,12,12} },
            //{ "geo", new List<int>{13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36} },
            { "bio", new List<int>{49,49,50,51,51,52,53,53,54,55,55,56,57,57,58,58,59,59,60,60} },
            { "geo", new List<int>{61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84} },
        };
    }
}