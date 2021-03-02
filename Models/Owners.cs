using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  [Table(@"Owners")]
  public class Owners
  {
    public Owners()
    {
            OwnersPropertys = new HashSet<OwnersPropertys>();
    }

    [Key]
    public string OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime AvailabilityFrom { get; set; }
    public DateTime AvailabilityTo { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public ICollection<OwnersPropertys> OwnersPropertys { get; set; }
  }
}