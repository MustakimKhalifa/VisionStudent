using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using VisionStudent.DTO.DTO;
using VisionStudent.DTO.Models;


namespace VisionStudent.Method
{
    public class SqlHelper
    {
        private static bool _boolValue = false;
        private static SqlConnection _connection = null;
        private static SqlCommand _command = null;
        private static DataSet _dataSet = null;
        private static DataTable _dataTable = null;
        private static SqlDataReader _dataReader = null;
        private static SqlDataAdapter _dataAdapter = null;
        private static string Connection = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        private static int index = 0;
        private static string path = string.Empty;
        public static void EstablishedConnection()
        {
            _connection = new SqlConnection(Connection);
        }

        public static void Open()
        {
            _connection.Open();
        }

        public static void Close()
        {
            _connection.Close();
        }

        public static DataSet GetStateList()
        {
            try
            {
                Open();
                _dataSet = new DataSet();
                _dataAdapter = new SqlDataAdapter("select StateId, StateName from State", _connection);
                _dataAdapter.Fill(_dataSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return _dataSet;
        }

        public static DataTable GetCityList(int stateId)
        {
            try
            {
                Open();
                _dataTable = new DataTable();
                _dataAdapter = new SqlDataAdapter("select CityId, CityName from City where StateId = " + stateId, _connection);
                _dataAdapter.Fill(_dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return _dataTable;
        }

        public static Student GetStudentById(int studentId)
        {
            var student = new Student();
            try
            {
                Open();
                _command = new SqlCommand();
                _command.CommandText = "USP_GetStudentById";
                _command.CommandType = CommandType.StoredProcedure;
                _command.Connection = _connection;
                _command.Parameters.AddWithValue("@StudentId", studentId);

                _dataReader = _command.ExecuteReader();

                student = GetMapStudent(_dataReader);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return student;
        }

        public static bool AddStudent(Student student)
        {
            _boolValue = false;
            try
            {
                Open();

                _command = new SqlCommand("insert into Student(Name,Address,Class,Age,Hobbies,Gender,StateId,CityId,PinCode,Photo)" +
                    "values(@name,@address,@class,@age,@hobbies,@gender,@stateid,@cityid,@pincode,@photo)", _connection);
                _command.CommandType = CommandType.Text;
                _command.Parameters.AddWithValue("@name", student.StudentName.Trim());
                _command.Parameters.AddWithValue("@address", student.Address);
                _command.Parameters.AddWithValue("@class", student.Class);
                _command.Parameters.AddWithValue("@age", student.Age);
                _command.Parameters.AddWithValue("@hobbies", student.Hobbies);
                _command.Parameters.AddWithValue("@gender", student.Gender);
                _command.Parameters.AddWithValue("@stateid", student.StateId);
                _command.Parameters.AddWithValue("@cityid", student.CityId);
                _command.Parameters.AddWithValue("@pincode", student.PinCode);
                _command.Parameters.AddWithValue("@photo", student.Photo);

                _command.ExecuteNonQuery();

                _boolValue = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return _boolValue;
        }

        public static bool UpdateStudent(Student student)
        {
            _boolValue = false;
            try
            {
                Open();

                _command = new SqlCommand();
                _command.CommandType = CommandType.Text;
                _command.CommandText = "Update Student " +
                    "set Name=@name,Address = @address ,Class =@class,Age=@age,Hobbies=@hobbies,Gender=@gender,CityId=@cityid,PinCode=@pincode,Photo=@photo" +
                    " where StudentId = " + student.Id;
                _command.Connection = _connection;
                _command.Parameters.AddWithValue("@name", student.StudentName);
                _command.Parameters.AddWithValue("@address", student.Address);
                _command.Parameters.AddWithValue("@class", student.Class);
                _command.Parameters.AddWithValue("@age", student.Age);
                _command.Parameters.AddWithValue("@hobbies", student.Hobbies);
                _command.Parameters.AddWithValue("@gender", student.Gender);
                _command.Parameters.AddWithValue("@cityid", student.CityId);
                _command.Parameters.AddWithValue("@pincode", student.PinCode);
                _command.Parameters.AddWithValue("@photo", student.Photo);

                _command.ExecuteNonQuery();

                _boolValue = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return _boolValue;
        }

        //public static List<GetStudentDtos> GetStudentList()
        //{
        //    List<GetStudentDtos> studentList = new List<GetStudentDtos>();
        //    try
        //    {
        //        Open();
        //        _command = new SqlCommand();
        //        _command.CommandText = "USP_GetStudent";
        //        _command.CommandType = CommandType.StoredProcedure;
        //        _command.Connection = _connection;
        //        _dataReader = _command.ExecuteReader();

        //        studentList = GetMapStudentList(_dataReader);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //    finally
        //    {
        //        Close();
        //    }
        //    return studentList;
        //}

        public static bool DeleteStudent(int studentId)
        {
            _boolValue = false;
            try
            {
                Open();
                _command = new SqlCommand();
                _command.CommandText = "Delete from Student where StudentId = " + studentId;
                _command.CommandType = CommandType.Text;
                _command.Connection = _connection;
                _command.ExecuteNonQuery();

                _boolValue = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return _boolValue;
        }

        //public static List<GetStudentDtos> GetMapStudentList(SqlDataReader response)
        //{
        //    List<GetStudentDtos> studentList = new List<GetStudentDtos>();

        //    while (response.Read())
        //    {
        //        GetStudentDtos student = new GetStudentDtos
        //        {
        //            Id = Convert.ToInt32(response["StudentId"].ToString()),
        //            StudentName = response["Name"].ToString(),
        //            Address = response["Address"].ToString(),
        //            Class = response["Class"].ToString(),
        //            Age = Convert.ToInt32(response["Age"].ToString()),
        //            Hobbies = response["Hobbies"].ToString().Trim(','),
        //            Gender = response["Gender"].ToString(),
        //            CityName = response["CityName"].ToString(),
        //            PinCode = Convert.ToInt32(response["PinCode"].ToString()),
        //            Photo = response["Photo"].ToString()
        //        };

        //        if (student.Photo != "")
        //        {
        //            index = student.Photo.IndexOf("\\Upload");
        //            path = student.Photo.Substring(index);
        //            student.Photo = path;
        //        }

        //        studentList.Add(student);
        //    }

        //    return studentList;
        //}

        private static Student GetMapStudent(SqlDataReader response)
        {
            Student student = new Student();


            while (response.Read())
            {
                student.Id = Convert.ToInt32(response["StudentId"].ToString());
                student.StudentName = response["Name"].ToString();
                student.Address = response["Address"].ToString();
                student.Class = response["Class"].ToString();
                student.Age = Convert.ToInt32(response["Age"].ToString());
                student.Hobbies = response["Hobbies"].ToString();
                student.Gender = response["Gender"].ToString();
                student.StateId = response["StateId"] == null ? 0 : Convert.ToInt32(response["StateId"].ToString());
                student.CityId = Convert.ToInt32(response["CityId"].ToString());
                student.PinCode = Convert.ToInt32(response["PinCode"].ToString());
                student.Photo = response["Photo"].ToString();
            }

            if (student.Photo != "")
            {
                index = student.Photo.IndexOf("\\Upload");
                path = student.Photo.Substring(index);
                student.Photo = path;
            }

            return student;
        }

        public static List<T> GetData<T>(string Procedure, Hashtable param = null)
        {
            List<T> List = new List<T>();
            try
            {
                Open();
                _command = new SqlCommand();
                _command.CommandText = Procedure;
                _command.CommandType = CommandType.StoredProcedure;
                _command.Connection = _connection;

                if (param != null)
                {
                    foreach (DictionaryEntry entry in param)
                    {
                        _command.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
                    }
                }

                _dataReader = _command.ExecuteReader();
                List = DataReaderMapToList<T>(_dataReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Close();
            }
            return List;
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
