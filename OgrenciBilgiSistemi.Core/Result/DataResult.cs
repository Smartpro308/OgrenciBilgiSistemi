using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Result
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(bool success, string message, string messageCode, T data) : base(success, message, messageCode)
        {
            Data = data;

        }

        public DataResult(bool success, string message, T data) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public DataResult(string message) : base(message)
        {

        }

        public DataResult(T data, string message, bool success) : base(success, message)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
