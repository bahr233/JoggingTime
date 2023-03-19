namespace JoggingTime.ViewModels
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool IsAuthorized { get; set; } = true;
        public ResponseViewModel()
        {

        }
        public ResponseViewModel(T data, string message = "", bool success = true, bool isAuthorized = true)
        {
            Data = data;
            Success = success;
            Message = message;
            IsAuthorized = isAuthorized;
        }
    }
}
