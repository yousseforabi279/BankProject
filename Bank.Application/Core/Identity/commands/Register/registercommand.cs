using Bank.Application.common.Results;
using Bank.Application.Core.Identity.Responses.Register;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Core.Identity.commands.Register
{
    public class registercommand:IRequest<Result<RegisterResponse>>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
