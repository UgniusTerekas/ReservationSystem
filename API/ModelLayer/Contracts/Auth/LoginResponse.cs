using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contracts.Auth
{
    public class LoginResponse
    {
        public string TokenJWT { get; set; }
    }
}
