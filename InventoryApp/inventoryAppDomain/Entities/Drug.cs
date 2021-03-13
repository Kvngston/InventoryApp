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
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DrugStatus CurrentDrugStatus { get; set; } = DrugStatus.NOT_EXPIRED;

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}