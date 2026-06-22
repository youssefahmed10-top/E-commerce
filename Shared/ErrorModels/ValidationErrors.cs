namespace Shared.ErrorModels
{
    public class ValidationErrors
    {
        public string field { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}