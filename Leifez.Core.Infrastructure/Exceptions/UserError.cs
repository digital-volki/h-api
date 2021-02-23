namespace Leifez.Core.Infrastructure.Exceptions
{
    public class UserError
    {
        public string Code { get; }
        public string Message { get; }
        public UserError(string message, string code)
        {
            Code = code;
            Message = message;
        }
    }
}
