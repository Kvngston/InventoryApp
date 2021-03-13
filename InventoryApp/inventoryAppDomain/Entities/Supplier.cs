using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventoryAppDomain.Entities
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int GrossAmountOfDrugsSupplied { get; set; }
        public string TagNumber { get; set; }


        public ICollection<Brand> Brands { get; set; }
    }
}