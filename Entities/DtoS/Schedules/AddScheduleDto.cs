using Entities.DtoS.Groups;
using Entities.DtoS.Subjects;
using Entities.DtoS.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DtoS.Schedules
{
    public class AddScheduleDto
    {
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
}
