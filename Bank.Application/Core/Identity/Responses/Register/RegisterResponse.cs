using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Core.Identity.Responses.Register
{
    internal class RegisterResponse
    {
        public string accesstoken { get; set; }
        public string refreshtoken { get; set; }

    }
}
