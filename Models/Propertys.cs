using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"Propertys")]
  public class Propertys
  {
      private enum TypeStatus{
        published=1, 
        available, 
        deleted
      }
      [Key]
      public Guid PropertyId { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public int RentalPriceId { get; set; }
      public int StatusId { get; set; }
      public Guid? TenantId { get; set; }

      public string Status{
        get{
          switch(StatusId)
          {
            case (int)TypeStatus.published:
              return "published";
              break;
            case (int)TypeStatus.available:
              return "available";
              break;
            case (int)TypeStatus.deleted:
              return "deleted";
              break;
            default:
              return "Error";
          }
        }
      }
      public Guid CreatedBy { get; set; }
      public DateTime CreatedOn { get; set; }
      public Guid? ModifiedBy { get; set; }
      public DateTime? ModifiedOn { get; set; }
  }
}
