using Core.Entities;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Warehouse : BaseEntity,IEntity
    {
        public int WareHouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsReadyForSale { get; set; }
    }
}
