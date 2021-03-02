using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace Homie_backend_test.Controllers
{

  [Authorize(AuthenticationSchemes = AuthSchemesAPI, Roles = "Partner")]
  [Route("propertys")]
  [ApiController]

  public class PropertysController : Controller
  {
    public const string AuthSchemesAPI = JwtBearerDefaults.AuthenticationScheme;

    private readonly Models.HomieContext _context;

    public PropertysController(Models.HomieContext context)
    {
      _context = context;
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<List<Models.Propertys>> PropertysPublished()
    {
      List<Models.Propertys> _propertys = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.RentalPrice.Active == true).Include(x => x.Status).Include(x => x.RentalPrice).Include(x => x.Tenant).ToList();
      if (_propertys != null)
        return StatusCode(StatusCodes.Status200OK, _propertys);
      else
        return NotFound();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Propertys> UpdateProperty(Models.Propertys property)
    {
      _context.Propertys.Update(property);
      if (_context.SaveChanges() > 0)
        return StatusCode(StatusCodes.Status200OK, property);
      else
        return BadRequest();
    }

    [HttpGet("{PropertyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Propertys> GetProperty(Guid propertyId)
    {

      Models.Propertys _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.RentalPrice.Active == true && propertycondition.PropertyId == propertyId).Include(x => x.Status).Include(x => x.RentalPrice).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
      _property.ModifiedBy = getPartner();
      _property.ModifiedOn = System.DateTime.Now;
      if (_property != null)
        return StatusCode(StatusCodes.Status200OK, _property);
      else
        return NotFound();
    }

    [HttpDelete("{PropertyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Models.Propertys> DeleteProperty(Guid PropertyId)
    {

      Models.Propertys _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.RentalPrice.Active == true && propertycondition.PropertyId == PropertyId).Include(x => x.Status).Include(x => x.RentalPrice).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
      if (_property != null)
      {
        _property.StatusId = (int)Models.Propertys.TypeStatus.deleted;
        _property.ModifiedBy = getPartner();
        _property.ModifiedOn = System.DateTime.Now;
        if (_context.SaveChanges() > 0)
        {
          _property = _context.Propertys.Where(propertycondition =>   propertycondition.RentalPrice.Active == true && propertycondition.PropertyId == PropertyId).Include(x => x.Status).Include(x => x.RentalPrice).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
          return StatusCode(StatusCodes.Status200OK, _property);
        }
        else
          return BadRequest();
      }
      else
        return NotFound();
    }

    private Guid getPartner()
    {
      if (this.User.Identities.FirstOrDefault().Name != null)
      {
        System.Security.Claims.ClaimsPrincipal currentUser = (System.Security.Claims.ClaimsPrincipal)this.User;
        return Guid.Parse(currentUser.Identities.FirstOrDefault().Name);


      }
      else
        return Guid.Empty;
    }

  }

}


