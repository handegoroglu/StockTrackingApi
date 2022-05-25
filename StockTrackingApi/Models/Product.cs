using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StockTrackingApi.Models
{
    public class Product
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Ürün adı
        /// </summary>
        [Required]
        [MaybeNull]
        [StringLength(128)]
        public string Name { get; set; }

        [MaybeNull]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [StringLength(256)]
        public string Barcode { get; set; } = string.Empty;

        public int StockCout { get; set; }

        [StringLength(1024)]
        public string? ProductImageUrl { get; set; }
    }
}
