using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Homie_backend_test.Controllers
{
  [Route("Authenticate/partners")]
  [ApiController]

  public class PartnerLoginController : Controller
  {

    private readonly Models.HomieContext _context;

    public PartnerLoginController(Models.HomieContext context)
    {
      _context = context;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Partners> Login(Models.PartnerLogin UserLogin)
    {
      if (UserLogin.user.Trim().Length > 0 && UserLogin.password.Trim().Length > 0)
      {
        string HomieKey=GenKey(50);
        UserLogin.password = EncryptText(UserLogin.password, HomieKey);
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
                new Claim(ClaimTypes.Name, UserLogin.user),
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

    #region Encrypt
    private String EncryptText(String Data, String sKey)
    {
      System.Security.Cryptography.TripleDESCryptoServiceProvider DES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
      Byte[] DatosByte;
      DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
      DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0, 8));
      DatosByte = ASCIIEncoding.ASCII.GetBytes(Data);
      System.Security.Cryptography.ICryptoTransform Crypto = DES.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(sKey), ASCIIEncoding.ASCII.GetBytes(sKey.Substring(0, 8)));
      System.IO.MemoryStream cipherStream = new System.IO.MemoryStream((Data.Length * 2) - 1);
      System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(cipherStream, Crypto, System.Security.Cryptography.CryptoStreamMode.Write);
      Byte[] bytesPlano = System.Text.Encoding.UTF8.GetBytes(Data);
      cryptoStream.Write(bytesPlano, 0, bytesPlano.Length);
      cryptoStream.FlushFinalBlock();
      Byte[] bytesEncriptados = new Byte[cipherStream.Length];
      cipherStream.Position = 0;
      cipherStream.Read(bytesEncriptados, 0, (int)cipherStream.Length);
      cryptoStream.Close();
      return Convert.ToBase64String(bytesEncriptados);
    }

    private String GenKey(double iterations)
    {
      String Resultado = String.Empty;
      double Valor = 0;
      for (int X = 0; X <= iterations; X++)
        Valor += (Math.Pow(-1, X)) / ((2 * X) + 1);
      Valor = Valor * 4;
      Resultado = (Valor.ToString().Replace(".", "").Replace(",", "") + Valor.ToString().Replace(".", "").Replace(",", ""));
      if (Resultado.Length < 24)
      {
        int mitad = Resultado.Length / 2;
        Resultado = (Resultado.Substring(0, mitad) + "Homie" + Resultado.Substring(mitad, Resultado.Length - mitad)).Substring(0, 24);
      }
      else
        Resultado = Resultado.Substring(0, 24);
      return Resultado;
    }

    #endregion
  }

}