using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.UI.Models
{
    public class Image
    {
        public string Path { get; set; }
        public int id { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
    }
}
