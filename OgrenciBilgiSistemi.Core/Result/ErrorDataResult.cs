using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, message, false)
        {
        }

        public ErrorDataResult(string message) : base(message)
        {

        }

        public ErrorDataResult(T data, string message, string messageCode) : base(false, message, messageCode, data)
        {
        }
    }
}
