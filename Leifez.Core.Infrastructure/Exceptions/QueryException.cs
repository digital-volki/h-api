using System;

namespace Leifez.Core.Infrastructure.Exceptions
{
    public class QueryException : Exception
    {
        public string Code { get; }
        public QueryException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
