using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleSphere.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Status {  get; set; }
        public dynamic? Result { get; set; }
    }
}
