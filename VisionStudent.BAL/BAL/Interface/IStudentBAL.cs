using System.Collections.Generic;
using System.Threading.Tasks;
using VisionStudent.DTO;
using VisionStudent.DTO.DTO;
using VisionStudent.DTO.Models;

namespace VisionStudent.BAL.Interface
{
    public  interface IStudentBAL
    {
        void EstablishConnection();
        bool AddStudent(Student student);
        bool DeleteStudent(int studentId);
        List<GetCityDtos> GetCityList(int stateId);
        List<GetStateDtos> GetStateList();
        Student GetStudentById(int studentId);
        Task<List<GetStudentDtos>> GetStudentListAsync();
        bool UpdateStudent(Student student);
    }
}
