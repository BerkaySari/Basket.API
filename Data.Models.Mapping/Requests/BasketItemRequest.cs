using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models.Mapping.Requests
{
    public class BasketItemRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        [MinLength(3)]
        public string ProductName { get; set; }

        [Required]
        [MinLength(2)]
        public string Supplier { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string DeliveryDateRange { get; set; }
        public string Color { get; set; }
    }
}
