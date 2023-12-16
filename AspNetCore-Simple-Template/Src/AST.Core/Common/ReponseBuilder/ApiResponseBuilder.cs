using System;
using System.Net;

namespace AST.Core.Common.ResponseBuilder
{
    public class ApiResponseBuilder<T>
    {
        private readonly ApiResponse<T> apiResponse;

        public ApiResponseBuilder()
        {
            apiResponse = new ApiResponse<T>();
            apiResponse.RequestId = Guid.NewGuid().ToString();
            apiResponse.Version = "1.0.0";
        }

        public ApiResponse<T> Build()
        {
            return apiResponse;
        }

        public ApiResponseBuilder<T> StatusCode(in int statusCode)
        {
            apiResponse.StatusCode = statusCode;
            return this;
        }

        public ApiResponseBuilder<T> Message(in string message)
        {
            if (message != null)
            {
                apiResponse.Message = message;
            }
            return this;
        }

        public ApiResponseBuilder<T> Result(in T result)
        {
            if (result != null)
            {
                apiResponse.Result = result;
            }
            return this;
        }

        public ApiResponseBuilder<T> DebugMessage(in string message)
        {
            if (message != null)
            {
                apiResponse.DebugMessage = message;
            }
            return this;
        }

        // Quick method
        public ApiResponseBuilder<T> Success(in T result, in string message = null)
        {
            StatusCode((int)HttpStatusCode.OK);
            if (message != null)
            {
                apiResponse.Message = message;
            }
            if (result != null)
            {
                apiResponse.Result = result;
            }
            return this;
        }

        public ApiResponseBuilder<T> Error(in string message = null)
        {
            StatusCode((int)HttpStatusCode.InternalServerError);
            if (message != null)
            {
                apiResponse.Message = message;
            }
            return this;
        }
    }
}
