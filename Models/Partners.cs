using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"Partners")]
  public class Partners
  {
      [Key]
      public Guid PartnerId { get; set; }
      public string Partner { get; set; }
      public string User { get; set; }
      public string Password { get; set; }
      public bool Active { get; set; }
  }
}