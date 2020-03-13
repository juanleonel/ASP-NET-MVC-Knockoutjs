using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Principal { 
    public partial class Book : BaseModel
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
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public string Isbn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public string Synopsis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        public DateTime Created_at { get; set; }
    }
}
