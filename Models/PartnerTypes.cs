using System;
using System.Collections.Generic;

namespace Homie_backend_test.Models
{
    public partial class PartnerTypes
    {
        public PartnerTypes()
        {
            Partners = new HashSet<Partners>();
        }

        public int PartnerTypeId { get; set; }
        public string PartnerType { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        public bool Active { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public ICollection<Partners> Partners { get; set; }
    }
}
