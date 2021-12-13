namespace Pishtova_ASP.NET_web_api.Model.OperationResult
{
    public class OperationResult<T> : VoidOperationResult
    {
        private T Data;

        public void SetData(T value)
        {
            this.Data = value;
        }
        public T GetData()
        {
            return this.Data;
        }
    }
}
