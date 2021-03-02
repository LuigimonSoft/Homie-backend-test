using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"Tenants")]
  public class Tenants
  {
    public Tenants()
    {
      Propertys = new HashSet<Propertys>();
    }

      [Key]
      public Guid TenantId { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      public string Phone { get; set; }
      public DateTime AvailabilityFrom { get; set; }
      public DateTime AvailabilityTo { get; set; }

      [Newtonsoft.Json.JsonIgnore]
      public ICollection<Propertys> Propertys { get; set; }
  }
}