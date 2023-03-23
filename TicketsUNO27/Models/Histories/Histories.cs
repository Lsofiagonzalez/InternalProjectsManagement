using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Core.Models.User
{
	public class Histories : Entity
    {
		public string HistoryName { get; set; }

        public string HistoryDescription { get; set; }

		public Guid ProjectsId { get; set; }

		[ForeignKey("ProjectsId")]

		public Projects Projects { get; set; }
	}
}