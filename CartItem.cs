using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;
using System.Windows.Forms;

namespace Pet___Beyonds
{
    internal class CartItem
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Image ProductImage { get; set; }
        public double Total { get; set; }

    }
    
}
