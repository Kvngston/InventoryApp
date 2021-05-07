using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventoryAppDomain.Entities
{
    public class DrugCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Amount { get; set; }

        public int DrugId { get; set; }
        public Drug Drug { get; set; }

        public int DrugCartId { get; set; }

        public DrugCart DrugCart { get; set; }
    }
}
