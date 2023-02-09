using System;
using System.Collections.Generic;
using System.Data;
using VisionStudent.DTO;
using VisionStudent.DTO.DTO;
using VisionStudent.Method;
using VisionStudent.DTO.Models;
using VisionStudent.BAL.Common;
using System.Threading.Tasks;
using VisionStudent.BAL.Interface;

namespace VisionStudent.BAL
{
    public class StudentBAL : IStudentBAL
    {
        private static DataSet _dataSet = null;
        private static DataTable _dataTable = null;
        public StudentBAL()
        {
        }
        public void EstablishConnection()
        {
            SqlHelper.EstablishedConnection();
        }
        public List<GetStateDtos> GetStateList()
        {
            List<GetStateDtos> stateList = new List<GetStateDtos>();
            _dataSet = new DataSet();
            _dataSet = SqlHelper.GetStateList();

            foreach (DataRow dataRow in _dataSet.Tables[0].Rows)
            {
                GetStateDtos state = new GetStateDtos
                {
                    StateId = Convert.ToInt32(dataRow["StateId"].ToString()),
                    StateName = dataRow["StateName"].ToString()
                };
                stateList.Add(state);
            }

            return stateList;
        }
        public List<GetCityDtos> GetCityList(int stateId)
        {
            List<GetCityDtos> cityList = new List<GetCityDtos>();
            _dataTable = new DataTable();
            _dataTable = SqlHelper.GetCityList(stateId);

            foreach (DataRow dataRow in _dataTable.Rows)
            {
                GetCityDtos city = new GetCityDtos
                {
                    CityId = Convert.ToInt32(dataRow["CityId"].ToString()),
                    CityName = dataRow["CityName"].ToString()
                };
                cityList.Add(city);
            }

            return cityList;
        }

        public bool AddStudent(Student student)
        {
            var response = SqlHelper.AddStudent(student);
            return response;
        }

        public bool UpdateStudent(Student student)
        {
            var response = SqlHelper.UpdateStudent(student);
            return response;
        }

        //public List<GetStudentDtos> GetStudent()
        //{
        //    var response = SqlHelper.GetStudentList();
        //    return response;
        //}        

        public async Task<List<GetStudentDtos>> GetStudentListAsync()
        {
            var response = SqlHelper.GetData<GetStudentDtos>(StoredProcedureName.USP_GetStudentList);
            return await Task.FromResult(response);
        }

        public bool DeleteStudent(int studentId)
        {
            var response = SqlHelper.DeleteStudent(studentId);
            return response;
        }

        public Student GetStudentById(int studentId)
        {
            var response = SqlHelper.GetStudentById(studentId);
            return response;
        }

    }
}