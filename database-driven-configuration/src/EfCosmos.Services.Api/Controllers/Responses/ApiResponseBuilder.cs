using System.Net;

namespace EfCosmos.Services.Api.Controllers.Responses
{
    public class ApiResponseBuilder
    {
        private readonly ApiResponse apiResponse;

        public ApiResponseBuilder()
        {
            apiResponse = new ApiResponse();
        }

        public ApiResponseBuilder Status(in bool status)
        {
            apiResponse.Status = status;
            return this;
        }

        public ApiResponseBuilder Code(in int code)
        {
            apiResponse.Code = code;
            return this;
        }

        public ApiResponseBuilder Message(in string message)
        {
            if (message != null)
            {
                apiResponse.Message = message;
            }
            return this;
        }

        public ApiResponseBuilder Data(in object data)
        {
            if (data != null)
            {
                apiResponse.Data = data;
            }
            return this;
        }

        public ApiResponse Build()
        {
            return apiResponse;
        }

        // Build template
        public ApiResponse BuildOk()
        {
            apiResponse.Status = true;
            apiResponse.Code = (int)HttpStatusCode.OK;
            apiResponse.Message = HttpStatusCode.OK.ToString();
            return apiResponse;
        }

        public ApiResponse BuildBadRequest()
        {
            apiResponse.Status = false;
            apiResponse.Code = (int)HttpStatusCode.BadRequest;
            apiResponse.Message = HttpStatusCode.BadRequest.ToString();
            return apiResponse;
        }

        public ApiResponse BuildInternalServerError()
        {
            apiResponse.Status = false;
            apiResponse.Code = (int)HttpStatusCode.InternalServerError;
            apiResponse.Message = HttpStatusCode.InternalServerError.ToString();
            return apiResponse;
        }

        public ApiResponse BuildNotFound()
        {
            apiResponse.Status = false;
            apiResponse.Code = (int)HttpStatusCode.NotFound;
            apiResponse.Message = HttpStatusCode.NotFound.ToString();
            return apiResponse;
        }

        public ApiResponse BuildForbidden()
        {
            apiResponse.Status = false;
            apiResponse.Code = (int)HttpStatusCode.Forbidden;
            apiResponse.Message = HttpStatusCode.Forbidden.ToString();
            return apiResponse;
        }

        public ApiResponse BuildUnauthorized()
        {
            apiResponse.Status = false;
            apiResponse.Code = (int)HttpStatusCode.Unauthorized;
            apiResponse.Message = HttpStatusCode.Unauthorized.ToString();
            return apiResponse;
        }
    }
}
