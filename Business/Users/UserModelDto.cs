using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Users
{
    public class UserModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; // The user's full name
        public string PhoneNumber { get; set; } = default!; // User's contact number

    }
}
