using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Result
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string MessageCode { get; }
     
        
        public Result(bool success, string message, string messageCode) : this(success, message)
        {
            Message = message;
            MessageCode = messageCode;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(string message)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
    }
}
