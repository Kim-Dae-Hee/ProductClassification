using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductClassification.Data
{
    public partial class Product
    {
        
        public string DateOnly
        {
            get
            {
                return CheckedDate.ToString("yyyy/MM/dd");
            }
        }
        
    }
}
