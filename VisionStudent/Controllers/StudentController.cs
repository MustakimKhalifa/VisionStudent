using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VisionStudent.DTO.Models;
using VisionStudent.BAL.Interface;

namespace VisionStudent.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentBAL _studentBAL;

        public StudentController(IStudentBAL studentBAL)
        {
            _studentBAL = studentBAL;
            _studentBAL.EstablishConnection();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            string FilePath = string.Empty;
            bool response = false;
            if (student.File != null)
            {
                FilePath = Upload(student.File);
            }

            student.Photo = FilePath;
            if (student.Photo != "")
            {
                var index = student.Photo.IndexOf("\\Upload");
                var path = student.Photo.Substring(index);
                student.Photo = path;
            }

            response = _studentBAL.AddStudent(student);
            if (response)
            {
                //ViewBag.Message = "insert successfully";
                TempData["Message"] = "insert successfully";
            }
            else
            {
                //ViewBag.Message = "Error in insert  !!";
                TempData["Message"] = "Error in insert  !!";
            }

            return RedirectToAction("GetStudentList");
        }

        [HttpPost]
        public ActionResult UpdateStudent(Student student)
        {
            string FilePath = string.Empty;
            bool response = false;
            if (student.File != null)
            {
                FilePath = Upload(student.File);
                student.Photo = FilePath;
            }

            if (student.Photo != "")
            {
                var index = student.Photo.IndexOf("\\Upload");
                var path = student.Photo.Substring(index);
                student.Photo = path;
            }

            response = _studentBAL.UpdateStudent(student);
            if (response)
            {
                //ViewBag.Message = "Update successfully";
                TempData["Message"] = "Update successfully";
            }
            else
            {
                //ViewBag.Message = "Error in update  !!";
                TempData["Message"] = "Error in update  !!";
            }

            return RedirectToAction("GetStudentList");
        }

        [HttpPost]
        public ActionResult GetStateList()
        {
            var response = _studentBAL.GetStateList();
            return Json(response);
        }

        [HttpPost]
        public ActionResult GetCityList(int stateId)
        {
            var response = _studentBAL.GetCityList(stateId);
            return Json(response);
        }

        [HttpGet]
        public ActionResult GetStudentList()
        {            
            return View();
        }

        //[HttpPost]
        //public ActionResult GetStudentListData()
        //{
        //    var response = _studentBAL.GetStudent();
        //    return Json(response);
        //}

        [HttpPost]
        public async Task<ActionResult> GetStudentListData()
        {
            var response = await _studentBAL.GetStudentListAsync();
            return Json(response);
        }

        [HttpGet]
        public ActionResult GetStudentById(int studentId)
        {
            var response = _studentBAL.GetStudentById(studentId);

            ViewBag.Class = response.Class;
            ViewBag.hobbies = response.Hobbies;
            ViewBag.cityid = response.CityId;

            return View(response);
        }

        [HttpGet]
        public ActionResult DeleteStudent(int studentId)
        {
            var response = _studentBAL.DeleteStudent(studentId);
            if (response)
            {
                TempData["Delete"] = "Delete successfully";
            }
            else
            {
                TempData["Delete"] = "Error in delete  !!";
            }
            return RedirectToAction("GetStudentList");
        }

        //private string ImageToBase64(string path)
        //{
        //    byte[] imageArray = System.IO.File.ReadAllBytes(path);
        //    string base64Image = Convert.ToBase64String(imageArray);
        //    return base64Image;
        //}

        private string Upload(HttpPostedFileBase file)
        {
            try
            {
                string Folder = "Upload";
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                    file.SaveAs(_path);
                    return _path;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}