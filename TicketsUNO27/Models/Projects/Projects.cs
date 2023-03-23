using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Core.Models.User
{
	public class Projects : Entity
    {
		public string ProjectName { get; set; }

        public string DisplayName { get; set; }

		public string ProjectDescription  { get; set; }
		public bool State { get; set; }


	}
}