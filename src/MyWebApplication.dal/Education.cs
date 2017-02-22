using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.dal
{
    [Table("Education")]
    public class Education
    {
        [Key]
        public int id { get; set; }

        public int UserId { get; set; }
        public string School { get; set; }
        public string YearAttended { get; set; }

        public User User { get; set; }
        
    }
}
