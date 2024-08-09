namespace ProductCatalog.Core.Models
{
    public class ErrorsModel
    {
        public List<ErrorModel> Errors { get; } = [];

        public ErrorsModel(ExecResult execResult)
        {
            foreach (var err in execResult.Errors)
            {
                Errors.Add(new ErrorModel { Message = err.Message, Code = err.Code });
            }
        }
    }

    public class ErrorModel
    {
        public string? Code { get; set; }
        public string Message { get; set; }
    }
}
