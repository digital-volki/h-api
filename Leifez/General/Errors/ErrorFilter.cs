using HotChocolate;
using Leifez.Core.Infrastructure.Exceptions;

namespace Leifez.General.Errors
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is QueryException)
            {
                var ex = error.Exception as QueryException;
                return error.WithCode(ex.Code).WithMessage(ex.Message).WithMessage(ex.StackTrace);
            }
            return error;
        }
    }
}
