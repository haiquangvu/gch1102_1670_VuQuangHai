using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookEshop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(150)]
        public string? ProductName { get; set; }
        [StringLength(3050)]
        public string? ProductDescription { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public decimal? ProductPrice { get; set; }
        public string? ProductPhoto { get; set;}
        [NotMapped]
        public IFormFile ProductImage { get; set; }
    }
}
 