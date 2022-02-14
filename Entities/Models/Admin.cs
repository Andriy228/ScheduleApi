using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
    }
}
