using System.ComponentModel.DataAnnotations;

namespace webapp04.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credits { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
