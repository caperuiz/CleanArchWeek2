using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL.Models
{
    public class CartDBModel
    {
        public string Id { get; set; }
        public List<CartItemDBModel> Items { get; set; } = new();
    }
}
