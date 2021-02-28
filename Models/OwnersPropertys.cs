using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"OwnersPropertys")]
  public class OwnersPropertys
  {
      [Key]
      public int OwnerPropertyId { get; set; }
      public string OwnerId { get; set; }
      public Guid? PropertyId { get; set; }
      public bool Active { get; set; }
      public DateTime CreatedOn { get; set; }
  }
}