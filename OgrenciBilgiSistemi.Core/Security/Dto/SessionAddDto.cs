using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Core.Security.Dto
{
    public class SessionAddDto
    {
        public int UserId { get; set; }
        public string TokenString { get; set; }
    }
}
