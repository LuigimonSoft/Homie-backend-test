using System;
using System.Text;

namespace Homie_backend_test
{
  public class Encrypt{
    public static String EncryptText(String Data, String sKey)
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

    public static  String GenKey(double iterations)
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
  }
}