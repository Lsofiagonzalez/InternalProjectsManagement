using Core.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Core.Models.User
{
	public class Files : Entity
	{
		public string FileName { get; set; }

		public string DisplayName { get; set; }

		public string FileDescription { get; set; }

		public string Url { get; set; }

        public string Extension{ get; set; }

        public Guid HistoriesId { get; set; }

		[ForeignKey("HistoriesId")]

		public Histories Histories  { get; set; }
	}

  
}