using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;


namespace TicketsUNO27.Models.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        private Accounts Accounts;

        //private AccountsModel am = new AccountsModel();

        public CustomPrincipal(Accounts accounts)
        {
            this.Accounts = accounts;
            this.Identity = new GenericIdentity(accounts.UserName);
        }
        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            return roles.Any(r => this.Accounts.Rols.RolName.Contains(r));
        }
    }
}