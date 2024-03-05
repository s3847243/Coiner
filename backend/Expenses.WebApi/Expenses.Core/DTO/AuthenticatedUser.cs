using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.Core.DTO
{
    public class AuthenticatedUser
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }

}
