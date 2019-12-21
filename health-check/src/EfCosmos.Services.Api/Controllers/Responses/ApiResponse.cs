namespace EfCosmos.Services.Api.Controllers.Responses
{
    public class ApiResponse
    {
        public bool Status { get; set; } = true;
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
