using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request.Email
{
    public class EmailSendRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string message { get; set; }
    }
}
