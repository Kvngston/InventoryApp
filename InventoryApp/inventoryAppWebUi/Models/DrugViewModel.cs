using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using inventoryAppDomain.Entities;
using inventoryAppDomain.Entities.Enums;


namespace inventoryAppWebUi.Models
{
    public class DrugViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DrugName { get; set; }


        [Required]
        [Display(Name = "Number of Units")]
        public int TotalUnitPerDrugs { get; set; }

            
        [Required]
        [Display(Name = "Price Per Unit")]
        public int PricePerUnit { get; set; }


        [Required]
        [Display(Name = "Quantity Supplied")]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }


        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string SupplierTag { get; set; }

        public List<DrugCategory> DrugCategory { get; set; }
        [Required]
        public int DrugCategoryId { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

}