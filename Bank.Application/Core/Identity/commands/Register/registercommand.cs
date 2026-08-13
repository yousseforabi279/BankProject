using Bank.Application.Core.Identity.Responses.Register;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Core.Identity.commands.Register
{
    internal class registercommand:IRequest<RegisterResponse>
    {
        public  MyProperty { get; set; }
    }
}
