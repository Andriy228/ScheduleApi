using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Student_in_group
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Group Group { get; set; }
    }
}
