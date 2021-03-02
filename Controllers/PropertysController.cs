using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Homie_backend_test.Controllers
{

   enum TypePartner{
     Error=0,
    PartnerBasic=1,
    Partner=2,
    PartnerFull=3

  }
  [Authorize(AuthenticationSchemes = AuthSchemesAPI, Roles = "Partner,Partnerfull,Partnerbasic")]
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
      List<Models.Propertys> _propertys=null;
      switch(getTypePartner())
      {
        case TypePartner.PartnerFull:
          _propertys = _context.Propertys.Include(x => x.Status).Include(x => x.RentalPrices).Include(x => x.Tenant).Where(propertycondition => propertycondition.StatusId == 1).ToList();
          break;
        case TypePartner.Partner:
          _propertys = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1).Include(x => x.Status).Include(x => x.RentalPrices).ToList();
          break;
        case TypePartner.PartnerBasic:
          _propertys = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 ).Include(x => x.Status).ToList();
          break;
      }
      if (_propertys != null)
        return StatusCode(StatusCodes.Status200OK, _propertys);
      else
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Propertys> CreateProperty(Models.Propertys property)
    {
      property.CreatedBy = getPartner();
      property.CreatedOn = System.DateTime.Now;
      Models.PropertysValidator validator = new Models.PropertysValidator();
      FluentValidation.Results.ValidationResult result = validator.Validate(property);
      if(result.IsValid){
        _context.Propertys.Update(property);
        if (_context.SaveChanges() > 0)
          return StatusCode(StatusCodes.Status201Created, property);
        else
          return BadRequest();
      }
      else 
      {
        List<Models.Errors> listErrors= new List<Models.Errors>();
        foreach(var failure in result.Errors) {
          Models.Errors Error= new  Models.Errors();
          Error.Error="Error in column " + failure.PropertyName + " failed validation";
          Error.Details = "Error: " + failure.ErrorMessage;
          listErrors.Add(Error);
        }
        return StatusCode(StatusCodes.Status400BadRequest, listErrors);
      }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Propertys> UpdateProperty(Models.Propertys property)
    {
      Models.PropertysValidator validator = new Models.PropertysValidator();
      FluentValidation.Results.ValidationResult result = validator.Validate(property);
      if(result.IsValid && property.PropertyId!=Guid.Empty){
        _context.Propertys.Update(property);
        if (_context.SaveChanges() > 0)
          return StatusCode(StatusCodes.Status200OK, property);
        else
          return BadRequest();
      }
      else 
      {
        List<Models.Errors> listErrors= new List<Models.Errors>();
        if(property.PropertyId!=Guid.Empty)
        {
          Models.Errors Error= new  Models.Errors();
          Error.Error="Error in column PropertyId failed validation";
          Error.Details = "Error: the PropertyId is required" ;
          listErrors.Add(Error);
        }

        foreach(var failure in result.Errors) {
          Models.Errors Error= new  Models.Errors();
          Error.Error="Error in column " + failure.PropertyName + " failed validation";
          Error.Details = "Error: " + failure.ErrorMessage;
          listErrors.Add(Error);
        }
        return StatusCode(StatusCodes.Status400BadRequest, listErrors);
      }
    }

    [HttpGet("{PropertyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Propertys> GetProperty(Guid propertyId)
    {

      Models.Propertys _property = null;
      switch(getTypePartner())
      {
        case TypePartner.PartnerFull:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == propertyId).Include(x => x.Status).Include(x => x.RentalPrices).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
          break;
        case TypePartner.Partner:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == propertyId).Include(x => x.Status).Include(x => x.RentalPrices).FirstOrDefault<Models.Propertys>();
          break;
        case TypePartner.PartnerBasic:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == propertyId).Include(x => x.Status).FirstOrDefault<Models.Propertys>();
          break;
      }
      
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
      Models.Propertys _property = null;
      switch(getTypePartner())
      {
        case TypePartner.PartnerFull:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == PropertyId).Include(x => x.Status).Include(x => x.RentalPrices).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
          break;
        case TypePartner.Partner:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == PropertyId).Include(x => x.Status).Include(x => x.RentalPrices).FirstOrDefault<Models.Propertys>();
          break;
        case TypePartner.PartnerBasic:
          _property = _context.Propertys.Where(propertycondition => propertycondition.StatusId == 1 && propertycondition.PropertyId == PropertyId).Include(x => x.Status).FirstOrDefault<Models.Propertys>();
          break;
      }if (_property != null)
      {
        _property.StatusId = (int)Models.Propertys.TypeStatus.deleted;
        _property.ModifiedBy = getPartner();
        _property.ModifiedOn = System.DateTime.Now;
        if (_context.SaveChanges() > 0)
        {
          _property = _context.Propertys.Where(propertycondition =>    propertycondition.PropertyId == PropertyId).Include(x => x.Status).Include(x => x.RentalPrices.Where(y=>y.Active)).Include(x => x.Tenant).FirstOrDefault<Models.Propertys>();
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

    private TypePartner getTypePartner()
    {
      if (this.User.Identities.FirstOrDefault().Name != null)
      {
        System.Security.Claims.ClaimsPrincipal currentUser = (System.Security.Claims.ClaimsPrincipal)this.User;
        string role=currentUser.Claims.ElementAt(2).Value;
        switch(role)
        {
          case "Partner":
            return TypePartner.Partner;
            break;
          case "Partnerbasic":
            return TypePartner.PartnerBasic;
            break;
            case "Partnerfull":
            return TypePartner.PartnerFull;
            break;
          default:
            return  TypePartner.Error;
            break;
        }


      }
      else
        return TypePartner.Error;
    }

  }

}


