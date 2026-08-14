using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.common.Results
{
    public enum ResultStatus
    {
        Success,
        ValidationError,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        RequiresTwoFactor,
        Failure,
        BadRequest,
        InternalServerError
    }
}
