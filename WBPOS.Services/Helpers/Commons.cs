using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WBPOS.Services.Helpers
{
    public enum Status
    {
        Active,
        Deactive,
        Deleted,
        Uploaded
    }

    public enum UserType
    {
        user,
        organization
    }
    public enum Message
    {
        Success,
        Failed 
    }
    public enum Protect
    {
       WBPOSGrowWithIndiaWinTechnologies
    }

    //Code to fetch the ImageURL
    public class ApplicationConfigurations
    {
        public string ImageBlobUrl { get; set; }
    }
    public static class Commons
    {
        public static string RandomString()
        {
            //var rBytes = new byte[24];
            //using (var crypto = new RNGCryptoServiceProvider()) crypto.GetBytes(rBytes);
            //var base64 = Convert.ToBase64String(rBytes);

            //// Generate Alphanumeric string:
            //var result = Regex.Replace(base64, "[A-Za-z0-9]", "");
            //another
            //var r = new Random();
            //string result = new String(Enumerable.Range(0, 24).Select(n => (Char)(r.Next(32, 127))).ToArray());

            Random RNG = new Random();
            const string range = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            var chars = Enumerable.Range(0, 6).Select(x => range[RNG.Next(0, range.Length)]);
            var result= new string(chars.ToArray());

            return result;
        }


        //private static CryptoStream EncryptStream(Stream responseStream)
        //{
        //    Aes aes = GetEncryptionAlgorithm();

        //    ToBase64Transform base64Transform = new ToBase64Transform();
        //    CryptoStream base64EncodedStream = new CryptoStream(responseStream, base64Transform, CryptoStreamMode.Write);
        //    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        //    CryptoStream cryptoStream = new CryptoStream(base64EncodedStream, encryptor, CryptoStreamMode.Write);

        //    return cryptoStream;
        //}

        //private static Stream DecryptStream(Stream cipherStream)
        //{
        //    Aes aes = GetEncryptionAlgorithm();

        //    FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
        //    CryptoStream base64DecodedStream = new CryptoStream(cipherSteam, base64Transform, CryptoStreamMode.Read);
        //    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        //    CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
        //    return decryptedStream;
        //}
        //private static Aes GetEncryptionAlgorithm()
        //{
        //    Aes aes = Aes.Create();
        //    //aes.GenerateKey();
        //    //aes.GenerateIV();
        //    aes.Key = secret_key;
        //    aes.IV = initialization_vector;

        //    return aes;
        //}
    }
    
}
