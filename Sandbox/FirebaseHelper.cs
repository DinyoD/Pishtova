namespace Sandbox
{
    using Newtonsoft.Json;
    using FireSharp.Config;
    using FireSharp.Response;
    using FireSharp.Interfaces;
    using FireSharp;
    using System.Collections.Generic;

    public class FirebaseHelper
    {
        private readonly IFirebaseConfig config;
        private readonly IFirebaseClient client;

        public FirebaseHelper(string authSecret, string basePath)
        {
            this.config = new FirebaseConfig()
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };
            this.client = new FirebaseClient(this.config);
        }

        // Geo-Eng-Bio
        public List<List<List<string>>> GetSubjectInfoFromFirebase(string colectionName)
        {
            FirebaseResponse fbResponce = this.client.Get(colectionName);
            var result = JsonConvert.DeserializeObject<List<List<List<string>>>>(fbResponce.Body.ToString());
            return result;
        }

        // Bulgarian
        public Dictionary<string,List<List<string>>> Get_BG_SubjectInfoFromFirebase(string colectionName)
        {
            FirebaseResponse fbResponce = this.client.Get(colectionName);
            var responce = fbResponce.Body.ToString().Split(",\"analiz")[0] +"}";
            return JsonConvert.DeserializeObject<Dictionary<string,List<List<string>>>> (responce);
            
        }

    }

}
