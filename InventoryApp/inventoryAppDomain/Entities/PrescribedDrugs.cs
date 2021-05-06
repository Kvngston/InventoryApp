using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventoryAppDomain.Entities
{
    public class PrescribedDrugs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DrugName { get; set; }

        [Required]
        public int TotalDosage { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int PrescriptionDuration { get; set; }

        [Required]
        public int MorningDosage { get; set; }

        [Required]
        public int AfternoonDosage { get; set; }

        [Required]
        public int EveningDosage { get; set; }

        [Required]
        public int TotalUnitPerDrugs { get; set; }

        [Required]
        public int PricePerUnit { get; set; }

        [Required]
        public int Quantity { get; set; }



        [Required]
        public string SupplierTag { get; set; }

        [Required]
        public int DrugCategoryId { get; set; }
        public DrugCategory DrugCategory { get; set; }
    }
}
