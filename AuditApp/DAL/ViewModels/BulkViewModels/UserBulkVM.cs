using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels.BulkViewModels
{
    public class UserBulkVM
    {
        public List<int> userIds { get; set; }
        public bool IsActive { get; set; }
    }
}
