using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SecurityTokenGenerator.Domain.Models
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public static BaseResponse<T> Success(T data) => new BaseResponse<T> { Data = data, IsSuccess = true, Message = default };
        public static BaseResponse<T> Error(string Message) => new BaseResponse<T> { Message = Message, IsSuccess = false };
    }
}
