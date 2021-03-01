using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Homie_backend_test.Models
{
  public class PartnerLogin
  {
      public string user { get; set; }
      public string password { get; set; }

  }
}