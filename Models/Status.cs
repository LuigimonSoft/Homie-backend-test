using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"Status")]
  public class Status
  {
      public Status()
        {
            Propertys = new HashSet<Propertys>();
        }
      
      public int StatusId { get; set; }
      public string StatusText { get; set; }
  
      [Newtonsoft.Json.JsonIgnore]
      public ICollection<Propertys> Propertys { get; set; }
  }
}