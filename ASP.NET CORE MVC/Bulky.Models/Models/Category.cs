using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Name should <=20 in length")]
        [DisplayName("Category Name")]
        public string Name { get; set; }=string.Empty;
        
        [DisplayName("Category Order")]
        [Range(1,100,ErrorMessage="Order should be in 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
