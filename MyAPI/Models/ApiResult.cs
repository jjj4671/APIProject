
namespace MyAPI.Models
{
    public class ApiResult<TValue>
    {
        public int StatusCode { get; set; }
        public string? StatusDescription { get; set; }
        public IEnumerable<TValue>? Response { get; set; }
    }
}