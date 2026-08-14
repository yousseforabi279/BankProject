using Bank.Application.common.Results;
using Bank.Application.Core.Identity.Responses.Register;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Core.Identity.commands.Login
{
    public class logincommand:IRequest<Result<RegisterResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
