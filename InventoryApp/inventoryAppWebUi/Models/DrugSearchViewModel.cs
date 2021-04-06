using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace inventoryAppWebUi.Models
{
    public class DrugSearchViewModel
    {
        public string SearchString { get; set; }
        public List<inventoryAppDomain.Entities.Drug> Drugs { get; set; }
    }
}