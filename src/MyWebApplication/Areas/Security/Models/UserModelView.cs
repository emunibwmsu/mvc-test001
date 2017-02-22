using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApplication.Areas.Security.Models
{
    public class UserModelView
    {
        public int Id { get; set; }
        [Required (ErrorMessage="This is a required field")]
        [MinLength(5, ErrorMessage="Minimum of 5 characters")]
        [MaxLength(20, ErrorMessage = "Maximum of 20 characters")]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? EmploymentDate { get; set; }
    }
}