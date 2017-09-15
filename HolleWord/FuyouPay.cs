using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HolleWord
{
    class FuyouPay
    {
        //private RSAParameters RSAKeyInfo;
        //private static RSACryptoServiceProvider priRSA = null;
        //private static RSACryptoServiceProvider pubRSA = null;
        private const string NET_PRIVATE_KEY = @"<RSAKeyValue><Modulus>7a772+S5AZjB7ZjDRcVdF2EKS/BjjfbwfZJMmN5bYsYMmphhBVunDUI3AnSEopymTX8A8Q1t2/zhE/vHdgSRBU2gHS5KPyqelSye7IF3Et1OiiGKS86s1FaBDH9m+AUT6taORr3BVoqotRtRbGmr0aJQA8VrvyRg4AEPx0gy2D0=</Modulus><Exponent>AQAB</Exponent><P>/Oz51dOb/Imzr9DenEqRGfLP5nRVQ0sNLBQXcTLHhRmP1ow5dYPWUArsI63E+Uv5p2IG+jM0SZ/rNK3whZSJiw==</P><Q>8JKURMVg6GqIleQq4e03uqEZ6AgErBlh2e+1/T9vgij6n/ueZysamHydZAupk3Wsfn1bkmdA4zqOCf7UZueOVw==</Q><DP>HDwIF8qrmyF0IahbcW8Ri6gDdWJ/MifqrIUBqO1WQJF98SFuOKQjBIRzn/gCCSJmGD1lMgENUTq88wCH3SGbyQ==</DP><DQ>nMS4MBR7cRkzRpI4S5+9CDZBXo4TFV84/78Q1iYHcUdpAuYCNHLHUwUpv29GIxy0unzQNpholWqr8uGH4kMU4w==</DQ><InverseQ>l6wYY08eCny831ITlc5uQ/MObAJ/UoNYw3jMHvLcoslAW25MPpPaitGCzn58CrgtzDSOVEY2isi84pTw2WZK7A==</InverseQ><D>gpNzQiaxjLMDNyiJfrcioUlqgrWZu9BB5nqNIh5mTilHm1bDVlI3wAz0c6DXjQ5KPqDbP5KFHCoc7QGRXsC7egNBX9kNtL7ZCuYw78pE5sNM4+885fgoqaBCbnc+PxgyAqQ+ZIO5u6QKXQpEoe7PpvxCVBAGyn/1klaQVidUivE=</D></RSAKeyValue>";
        private const string NET_PUBLIC_KEY = @"<RSAKeyValue><Modulus>3KfTVNvRH6j0XiSxgUtFmcAisTka3OCEKSm7qdXBeCs0J6TAsHCVxfSltbyf2czjMAJRn4atlfejBjzjuk/2ZHrGrI3ZstfcJzncw8Rl7tqRDGxTAzqJ2MX9Zuf1TbNvk2dfHslufOV3n16DZWAs4OkSZlytH2dVDwFPWcYKHBE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        

        public static string signData(string dataToBeSigned)
        {
            byte[] data = Encoding.UTF8.GetBytes(dataToBeSigned);

            RSACryptoServiceProvider priRSA = new RSACryptoServiceProvider();
            priRSA.FromXmlString(NET_PRIVATE_KEY);

            var pri = priRSA;

            byte[] endata = priRSA.SignData(data, "SHA1");

            return Convert.ToBase64String(endata);

        }

        public static bool verifySignature(string signature, string signedData)
        {
            byte[] sign = Convert.FromBase64String(signature);
            return verifySignature(sign, signedData);
        }

        public static bool verifySignature(byte[] signature, string signedData)
        {
            try
            {
                byte[] hash = Convert.FromBase64String(signedData);

                RSACryptoServiceProvider pubRSA = new RSACryptoServiceProvider();
                pubRSA.FromXmlString(NET_PUBLIC_KEY);

                var pub = pubRSA;
                if (pubRSA.VerifyData(hash, "SHA1", signature))
                {
                    return true;
                }
                else
                {
                    //Console.WriteLine("The signature is not valid.");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        static void Main(string[] args)
        {
            string signString = "100|http://account.xzz-test.com/Cards/Recharge|13678424821|0002900F0006944|11032302065863805732|http://account.xzz-test.com/Cards/Recharge";

            string sign = signData(signString);


            verifySignature(sign, signString);
        }
    }
}
