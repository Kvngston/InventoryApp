using inventoryAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace inventoryAppWebUi.Models
{
    public class DrugPrescriptionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DrugName { get; set; }
        
        [Required]
        [Display(Name = "Quantity")]
        public int TotalDosage { get; set; }
        [Required]
        [Display(Name = "Price per dosage")]
        public int PricePerUnit { get; set; }

        [Required]
        [Display(Name = "Total price")]
        public int TotalPrice { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public int PrescriptionDuration { get; set; }

        [Required]
        [Display(Name = "Morning")]
        public int MorningDosage { get; set; }

        [Required]
        [Display(Name = "Afternoon")]
        public int AfternoonDosage { get; set; }

        [Required]
        [Display(Name = "Evening")]
        public int EveningDosage { get; set; }
        //public List<Drug> Drug { get; set; }
        //[Required]
        //public int DrugId { get; set; }
        public string UserId { get; set; }

    }

    //public class DrugNameViewModel
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Required]
    //    public string DrugName { get; set; }


    //    [Required]
    //    [Display(Name = "Unit per drug")]
    //    public int TotalUnitPerDrugs { get; set; }


    //    [Required]
    //    [Display(Name = "Unit per drug")]
    //    public int PricePerUnit { get; set; }


    //    [Required]
    //    [Display(Name = "Quantity Supplied")]
    //    public int Quantity { get; set; }

    //    [Required]
    //    public decimal Price { get; set; }


    //    [DataType(DataType.Date, ErrorMessage = "Date only")]
    //    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //    [Required]
    //    public DateTime ExpiryDate { get; set; }

    //    [Required]
    //    public string SupplierTag { get; set; }
    //}

}