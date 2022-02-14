using Entities.DtoS.Groups;
using Entities.DtoS.Students;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DtoS.StudentInGroup
{
    public class GetStudentInGroupDto
    {
        public int Id { get; set; }
        public virtual GetGroupDto Group { get; set; }
        public virtual GetStudentDto Student { get; set; }
    }
}
