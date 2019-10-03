using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10Authentication.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public AisleType Type { get; set; }

        [Range(0, (double)Decimal.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, 1000)]
        public int Quantity { get; set; }



        public int CartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
