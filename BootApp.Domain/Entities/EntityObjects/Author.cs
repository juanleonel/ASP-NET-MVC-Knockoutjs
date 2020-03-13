using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities.Principal
{
	public partial class Author : BaseModel
    {
        /// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
        public int Id { get; set; }

        /// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
        public string FirstName { get; set; }

        /// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
        public string LastName { get; set; }

        /// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
        public string Biography { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime Created_at { get; set; }
    }
}
