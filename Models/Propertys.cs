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
      public  enum TypeStatus{
        published=1, 
        available, 
        deleted
      }



      public Propertys()
        {
          OwnersPropertys = new HashSet<OwnersPropertys>();
          RentalPrices = new HashSet<RentalPrices>();
            
        }

        public Guid PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public Guid? TenantId { get; set; }

        [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ICollection<RentalPrices> RentalPrices { get; set; }

        [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Status Status { get; set; }

        [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Tenants Tenant { get; set; }

        [Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonIgnore]
        public ICollection<OwnersPropertys> OwnersPropertys { get; set; }


      [Newtonsoft.Json.JsonIgnore]
      public Guid CreatedBy { get; set; }

      [Newtonsoft.Json.JsonIgnore]
      public DateTime CreatedOn { get; set; }

      [Newtonsoft.Json.JsonIgnore]
      public Guid? ModifiedBy { get; set; }

      [Newtonsoft.Json.JsonIgnore]
      public DateTime? ModifiedOn { get; set; }
  }
}
