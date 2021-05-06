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
        public int PricePerUnit { get; set; }

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
    }
}