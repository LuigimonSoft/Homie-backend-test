using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Homie_backend_test.Controllers
{
  [Authorize]
  [Route("Authenticate/partners")]
  [ApiController]

  public class PartnerLoginController : Controller
  {

    private readonly Models.HomieContext _context;

    public PartnerLoginController(Models.HomieContext context)
    {
      _context = context;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Partners> Login(Models.PartnerLogin UserLogin)
    {
      if (UserLogin.user.Trim().Length > 0 && UserLogin.password.Trim().Length > 0)
      {
        string HomieKey=Encrypt.GenKey(50);
        UserLogin.password = Encrypt.EncryptText(UserLogin.password, HomieKey);
        Models.Partners partner = _context.Partners.SingleOrDefault(partnercondition => partnercondition.User == UserLogin.user && partnercondition.Password == UserLogin.password && partnercondition.Active == true);
        if (partner != null)
        {
          var tokenHandler = new JwtSecurityTokenHandler();

          var key = Encoding.ASCII.GetBytes(HomieKey);

          SecurityTokenDescriptor tokenDescriptor = null;

          string TipoUsuario = "Partner";

          tokenDescriptor = new SecurityTokenDescriptor
          {
            Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.UserData, UserLogin.user),
                new Claim(ClaimTypes.Name, partner.PartnerId.ToString()),
                new Claim(ClaimTypes.Role, TipoUsuario)

              }),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
          };

          var token = tokenHandler.CreateToken(tokenDescriptor);
          partner.Token = tokenHandler.WriteToken(token);
          return StatusCode(StatusCodes.Status200OK, partner);
        }
        else
          return StatusCode(StatusCodes.Status401Unauthorized);
      }
      else 
        return BadRequest();
    }

    
  }

}