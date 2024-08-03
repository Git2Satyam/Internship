using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.Models
{
    public class ResponseModel
    {
        public string? Status { get; set; }
        public bool? Success { get; set; }
        public dynamic? Result { get; set; }
    }
}
