using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.Test
{
    public  class TestPattern
    {
        public Dictionary<string, List<int>> SubjectsID = new()
        {
            { "bel_12", new List<int>{98,98,98,98,98,99,99,99,100,100,101,101,101,101,101,101,101,101,101,101} },
            { "bel_7", new List<int>{111,112,112,113,114,115,115,116,117,118,119,119,119,119,119,119,119,119,119,119} },
            { "bio", new List<int>{49,49,50,51,51,52,53,53,54,55,55,56,57,57,58,58,59,59,60,60} },
            { "geo", new List<int>{61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84} },
            { "eng", new List<int>{85,86,87,88,89,90,91,92,92,93,94,95,96,96,97} },
        };
    }
}