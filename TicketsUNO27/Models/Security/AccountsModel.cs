using AppRecordatorio.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TicketsUNO27.Models.Security
{
    public class AccountsModel
    {
        private Context db = new Context();

        private List<Accounts> ListAccounts = new List<Accounts>();


        public Accounts find(Guid? Id = null)
        {
            var Users = db.Users.Where(x => x.Id == Id).Include(x => x.Rol).FirstOrDefault();
            Accounts UserAccount = new Accounts
            {
                UserName = Users.UserName + " " + Users.LastName,
                Rols = Users.Rol,
                Id = Users.Id
            };
            return UserAccount;
        }


        public Accounts Login(string Documento, string Password)
        {
            var Users = db.Users.Where(c => c.CC == Documento && c.Password == Password).Include(x => x.Rol).FirstOrDefault();
            if (Users != null)
            {
                Accounts UserAccount = new Accounts
                {
                    UserName = Users.UserName + " " + Users.LastName,
                    Rols = Users.Rol,
                    Id = Users.Id
                };
                return UserAccount;
            }
            {
                return null;
            }

        }
    }
}