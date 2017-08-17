using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class Order_prod
    {
        [Key]
        public int id_ord { get; set; }
        public virtual Order Order { get; set; }

        [Key]
        public int id_pr { get; set; }
        public virtual Product Product { get; set; }
    }
}
