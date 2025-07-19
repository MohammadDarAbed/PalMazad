using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class CommissionSettingEntity : BaseEntity
    {
        public decimal Percentage { get; set; } // Commission percentage (e.g., 0.1 = 10%)
        public DateTime ActiveFromDate { get; set; } // Start date for this commission setting
    }

}
