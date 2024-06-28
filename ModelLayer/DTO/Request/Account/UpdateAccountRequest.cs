using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Account
{
    public class UpdateAccountRequest
    {
        public int? AccountId { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
