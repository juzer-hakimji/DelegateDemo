using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelegateDemo.Models
{
    public class DBContextResult<U>
    {
        public U Data { get; set; }
        public bool TransactionResult { get; set; }
    }
}
