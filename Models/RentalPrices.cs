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
    [Key]
    public int RentalPriceId { get; set; }
    public decimal RentalPrice { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedOn { get; set; }
  } 
}