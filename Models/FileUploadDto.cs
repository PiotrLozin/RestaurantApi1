using System.ComponentModel.DataAnnotations;

namespace RestaurantApi.Models
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
    }

}
