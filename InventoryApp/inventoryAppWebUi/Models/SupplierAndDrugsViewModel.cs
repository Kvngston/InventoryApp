using inventoryAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventoryAppWebUi.Models
{
    public class SupplierAndDrugsViewModel
    {
        public SupplierViewModel SupplierViewModel { get; set; }
        public IEnumerable<DrugViewModel> DrugViewModel { get; set; }
    }
}