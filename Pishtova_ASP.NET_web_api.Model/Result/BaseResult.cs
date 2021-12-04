using System;
using System.Collections.Generic;

namespace Pishtova_ASP.NET_web_api.Model.Result
{
    public abstract class BaseResult
    {
        public BaseResult()
        {
            this.ErrorsMessages = new HashSet<string>();
        }
        public ICollection<string> ErrorsMessages { get; set; }

        public bool IsSuccessfull => this.ErrorsMessages.Count == 0;
    }
}
