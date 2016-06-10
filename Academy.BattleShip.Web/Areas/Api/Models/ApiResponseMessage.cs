using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;

namespace Academy.BattleShip.Web.Areas.Api.Models
{
    /// <summary>
    /// Default api response data
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Type of error occured
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// Message that represent an short explanation about error
        /// </summary>
        public string ErrorMessage { get; private set; }
        
        /// <summary>
        /// If api should return some data, api data will be here
        /// </summary>
        public T ResponseData { get; private set; }

        public ApiResponse()
        {

        }

        public ApiResponse(string error, string message)
        {
            Error = error;
            ErrorMessage = message;
        }

        public ApiResponse(T responseData)
        {
            ResponseData = responseData;
        }
    }

    public static class ApiResponseExtensions
    {
        public static HttpResponseMessage CreateApiResponse<T>(this HttpRequestMessage request, T responseData)
        {
            var data = new ApiResponse<T>(responseData);
            return request.CreateResponse(HttpStatusCode.OK, data);
        }

        public static HttpResponseMessage CreateApiResponse(this HttpRequestMessage request, string error, string message)
        {
            var data = new ApiResponse<object>(error, message);
            return request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}