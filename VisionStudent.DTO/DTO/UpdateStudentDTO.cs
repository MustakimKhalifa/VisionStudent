using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionStudent.DTO.DTO
{
    public class UpdateStudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        public string Hobbies { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public int PinCode { get; set; }
        public string Photo { get; set; }
    }
}
