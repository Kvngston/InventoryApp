using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using inventoryAppDomain.Entities.Enums;

namespace inventoryAppDomain.Entities
{
    public class Drug
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string DrugName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DrugStatus CurrentDrugStatus { get; set; } = DrugStatus.NOT_EXPIRED;

        [Required]
        public string SupplierTag { get; set; }

        [Required]
        public int DrugCategoryId { get; set; }
        [Required]
        public DrugCategory DrugCategory { get; set; }
        
    }
}