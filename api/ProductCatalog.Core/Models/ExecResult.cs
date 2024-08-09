using Microsoft.AspNetCore.Identity;

namespace ProductCatalog.Core.Models
{
    public class ExecResult
    {
        public List<ExecError> Errors { get; } = [];

        public bool Succeeded { get => Errors.Count == 0; }

        public void AddError(string message, string? code = null)
        {
            Errors.Add(new ExecError
            {
                Message = message,
                Code = code,
            });
        }

        public void AddErrors(IdentityResult identityResult)
        {
            foreach (var err in identityResult.Errors)
            {
                Errors.Add(new ExecError
                {
                    Message = err.Description,
                    Code = err.Code,
                });
            }
        }

        public void AddErrors(ExecResult execResult)
        {
            foreach (var err in execResult.Errors)
            {
                Errors.Add(new ExecError
                {
                    Message = err.Message,
                    Code = err.Code,
                });
            }
        }
    }

    public class ExecResult<T> : ExecResult
    {
        public T Result { get; set; }
    }

    public class ExecError
    {
        public string Message { get; set; }
        public string? Code { get; set; }
    }
}
