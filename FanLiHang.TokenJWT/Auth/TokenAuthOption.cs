
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace tokenJWT.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "ExampleAudience";
        public static string Issuer { get; } = "ExampleIssuer";
        public static string SecretKey { get; } = "fanlihang";
        //public static string SecretKey { get; } = new ConfigurationBuilder().Build().GetValue<string>("secretKey");
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromDays(1);
    }
}
