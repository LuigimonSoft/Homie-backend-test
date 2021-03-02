using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"RentalPrices")]
  public class RentalPrices
  {
    public RentalPrices()
        {
            Propertys = new HashSet<Propertys>();
        }

    [Newtonsoft.Json.JsonIgnore]
    public int RentalPriceId { get; set; }
    public decimal RentalPrice { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public bool Active { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    public DateTime CreatedOn { get; set; }

      [Newtonsoft.Json.JsonIgnore]
      public ICollection<Propertys> Propertys { get; set; }
  } 
}