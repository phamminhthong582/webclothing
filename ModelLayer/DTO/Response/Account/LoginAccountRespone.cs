using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Response.Account
{
    public class LoginAccountRespone
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
