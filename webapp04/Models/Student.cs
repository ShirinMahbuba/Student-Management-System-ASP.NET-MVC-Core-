using System.ComponentModel.DataAnnotations;

namespace webapp04.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Range(30001, 99999)]
        public int StudentRoll { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;

        public string? PhotoPath { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
