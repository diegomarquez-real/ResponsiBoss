using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Api.Models
{
    public class AuthTokenModel
    {
        public System.Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}