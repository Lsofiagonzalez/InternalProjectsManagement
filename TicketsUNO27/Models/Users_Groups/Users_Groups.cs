using System;
using Core.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.User
{
    public class Users_Groups: Entity
    {

            public Guid UserId { get; set; }
            [ForeignKey("UserId")]
            public Users User { get; set; }

            public Guid GroupId { get; set; }
            [ForeignKey("GroupId")]
            public Groups Groups { get; set; }
            public Guid RolId { get; set; }
            [ForeignKey("RolId")]
            public Rols Rol { get; set; }





    }
    
}