using Core.Models.User;
using System;

namespace TicketsUNO27.Models.Security
{
    public class Accounts
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Rols Rols { get; set; }
    }
}
