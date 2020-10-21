using System.Text.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CpStnPRS.Models
{
    public class RequestLine
    {
        public int Id { get; set; }       
        public int RequestId { get; set; }
        [JsonIgnore]
        public virtual Request Request { get; set; } 
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; } = 1;
      

        public RequestLine() { }
    }
}
