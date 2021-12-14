namespace Pishtova_ASP.NET_web_api.Model.OperationResult
{
    using System.Collections.Generic;

    public interface IVoidOperationResult
    {
        public ICollection<string> GetErrorMessages { get; }

        public bool AddErrorMessage(string errorMessage);

        public bool AddErrorMessages(ICollection<string> errorMessages);

        public bool IsSuccessful { get; }
    }

    public class VoidOperationResult : IVoidOperationResult
    {
        private readonly ICollection<string> ErrorMessages;
        public VoidOperationResult()
        {
            this.ErrorMessages = new HashSet<string>();
        }

        public ICollection<string> GetErrorMessages => this.ErrorMessages;

        public bool AddErrorMessage(string errorMessage) {
            if (errorMessage == null) return false;
            this.ErrorMessages.Add(errorMessage);
            return true;
        }
        public bool AddErrorMessages(ICollection<string> errorMessages)
        {
            if (errorMessages.Count == 0) return false;
            foreach (var err in errorMessages)
            {
                this.ErrorMessages.Add(err);
            }
            return true;
        }
        public bool IsSuccessful => this.ErrorMessages.Count == 0;
    }
}
