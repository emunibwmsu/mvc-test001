using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApplication.dal
{
    public class User
    {
        public User()
        {
            Educations = new List<Education>();
        }

        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? EmploymentDate { get; set; }

        public ICollection<Education> Educations { get; set; }
    }
}
