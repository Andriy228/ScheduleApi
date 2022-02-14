using Entities.DtoS.Groups;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DtoS.Schedules
{
    public class GetScheduleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual GetGroupDto Group { get; set; }
        public virtual GetTeacherDto Teacher { get; set; }
        public virtual GetSubjectDto Subject { get; set; }
    }
}
