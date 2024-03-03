using OgrenciBilgiSistemi.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciBilgiSistemi.Entity.Entitites
{
    public class UserSession : IEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public byte ApplicationUserType { get; set; }

    }
}
