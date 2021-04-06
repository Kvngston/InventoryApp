using inventoryAppDomain.IdentityEntities;
using inventoryAppDomain.Services;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using inventoryAppDomain.Entities.Enums;
using Microsoft.AspNet.Identity.Owin;

namespace inventoryAppDomain.Entities
{
    public class DrugCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<DrugCartItem> DrugCartItems { get; set; }

        public CartStatus CartStatus { get; set; } = CartStatus.ACTIVE;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
