using System;
using Core.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.User;

namespace Core.Models.User
{
    public class Rols : Entity
	{
		public string RolName { get; set; }

		public string Description { get; set; }

		public string DisplayName { get; set; }

		//public string ParentId { get; set; }
		public string ParentId { get; set; }

	}
}