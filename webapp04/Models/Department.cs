using System.ComponentModel.DataAnnotations;

namespace webapp04.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Name { get; set; } = string.Empty;

    }
}
