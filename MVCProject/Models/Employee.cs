using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20,ErrorMessage ="Max Length is 20 letters")]
        [MinLength(3,ErrorMessage ="Min Length is 3 letters")]
        [UniqueName]
        public string Name { get; set; }


        [Range(1000, 100000 , ErrorMessage ="Value must be in range 1000:100000")]
        public int Salary { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string? Address { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

    }
}
