namespace Pishtova_ASP.NET_web_api.Model.OperationResult
{
    public interface IOperationResult<T>
    {
        public void SetData(T value);

        public T GetData { get; }
    }

    public class OperationResult<T> : VoidOperationResult, IOperationResult<T>
    {
        private T Data;

        public void SetData(T value)
        {
            this.Data = value;
        }

        public T GetData => this.Data;
    }
}
