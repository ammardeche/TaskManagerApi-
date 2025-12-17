using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public class ApiResponse<T>
    {

        public bool Success { get; set; } // true if request succeeded 
        public string Message { get; set; } // message for front end 

        public T data { get; set; }

        // static helper for success
        public static ApiResponse<T> SuccesResponse(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Success = true,
                data = data,
                Message = message
            };

        }

        // static helper for success
        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                data = default,
                Message = message
            };

        }

    }
}