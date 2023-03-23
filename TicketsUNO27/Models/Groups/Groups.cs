using System;
using Core.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.User;

namespace Core.Models.User
{
    public class Groups: Entity
    {

            public string GroupName { get; set; }
            public Guid ProjectId { get; set; }
            [ForeignKey("ProjectId")]
            public Projects Projects { get; set; }






    }
    
}