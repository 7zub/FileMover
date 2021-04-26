namespace FileMover.model
{
    public class Response
    {
        public enum RespType
        {
            Success,
            Warning,
            Error
        }

        public RespType respType { get; set; }
        public string Message { get; set; }
    }
}
