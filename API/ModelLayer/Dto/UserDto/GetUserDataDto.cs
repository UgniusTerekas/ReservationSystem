using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Dto.UserDto
{
    public class GetUserDataDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string State { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
