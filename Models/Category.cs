using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Categoty Name required.*")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "DisplayOrder Number required.*")]
        [Range(1,100,ErrorMessage ="Display Order Number Must be between 1-100.*")]
        public int DisplayOrder { get; set; }    
    }
}
