using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }

        [Required]

        public string DeptName { get; set; }
    }
}
