using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VisionStudent.DTO.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Display(Name ="Name")]
        [RegularExpression("^[A-Za-z][A-Za-z0-9_ ]{2,100}$",ErrorMessage = "Name must start with alphabet & contain alphabet, number and _ only. with more than 2 length")]
        public string StudentName { get; set; }
        [MaxLength(100,ErrorMessage = "{0} can have a max of {1} characters")]
        public string Address { get; set; }
        public string Class { get; set; }
        [Range(1,100,ErrorMessage ="{0} must be in {1} to {2} range")]
        public int Age { get; set; }
        public string Hobbies { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        [RegularExpression("^[0-9]{6,6}$",ErrorMessage ="Pincode must be of 6 digit")]
        public int PinCode { get; set; }
        public string Photo { get; set; }
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:()])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image files allowed.")]
        public HttpPostedFileBase File { get; set; }
    }
}
