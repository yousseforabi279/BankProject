using bank.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Presistence.Dbcontext
{
    public class Appuser: IdentityUser
    {
        public Customer? Customer { get; set; }
        public ICollection<Account> Accounts { get;  } = new List<Account>();
        public ICollection<Disposition> Dispositions { get; }
        = new List<Disposition>();
    }
}
