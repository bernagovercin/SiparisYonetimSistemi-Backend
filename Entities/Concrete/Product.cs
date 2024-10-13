using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : BaseEntity,IEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string ProductName { get; set; }
        public SizeEnum Size { get; set; }
       

    }
}
