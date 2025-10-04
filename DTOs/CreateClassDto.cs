using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class CreateClassDto
    {
        [Required(ErrorMessage = "Class name is required")]
        public string Name { get; set; }
    }
}
