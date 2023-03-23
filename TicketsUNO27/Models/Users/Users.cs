using Core.Models.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core.Models.User
{
    public class Users : Entity
    {
        
        public string UserName { get; set; }

        public string LastName { get; set; }

        public int NumberPhone{ get; set; }
        public string CC { get; set; }

        public string Password { get; set; }

        public string ProfilePhoto { get; set; }

        public bool State { get; set; }

        public Guid RolId  { get; set; }
        [ForeignKey("RolId")]
        public Rols Rol { get; set; }



    }
}