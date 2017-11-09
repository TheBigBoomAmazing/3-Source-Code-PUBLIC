using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace eContract.DDP.Common
{
    /// <summary>
    /// 和加密有关的函数
    /// </summary>
    public class Cryptography
    {
        /// <summary>
        /// ???该放在什么地方呢
        /// </summary>
        public const string EncryptKey = "css123";

        /// <remarks>
        /// Depending on the legal key size limitations of a specific CryptoService provider
        /// and length of the private key provided, padding the secret key with space character
        /// to meet the legal size of the algorithm.
        /// </remarks>
        private static byte[] GetLegalKey(string Key)
        {
            //具有随机密钥的 DES 实例
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len = des.LegalKeySizes[0].MinSize / 8;
            string sTemp = Key;
            if (sTemp.Length > len)
                sTemp = sTemp.Substring(0, len);
            else
            {
                while (sTemp.Length < len)
                    sTemp += ' ';
            }
            // convert the secret key to byte array
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 根据Key加密字符串，返回加密后的字符串
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string EncryptString2String(string strSource, string strKey)
        {
            Byte[] baInput = Encoding.Unicode.GetBytes(strSource);

            //具有随机密钥的 DES 实例
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] bytKey = GetLegalKey(strKey);

            // set the private key
            des.Key = bytKey;
            des.IV = bytKey;

            //从此实例创建 DES 加密器
            ICryptoTransform desencrypt = des.CreateEncryptor();

            Byte[] baOutput = desencrypt.TransformFinalBlock(baInput, 0, baInput.Length);

            return Convert.ToBase64String(baOutput);
        }

        /// <summary>
        /// 根据Key解密
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string DecryptString2String(string strSource, string strKey)
        {
            byte[] baInput = Convert.FromBase64String(strSource);
            //具有随机密钥的 DES 实例
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] bytKey = GetLegalKey(strKey);

            // set the private key
            des.Key = bytKey;
            des.IV = bytKey;

            //从此 des 实例创建 DES 解密器
            ICryptoTransform desdecrypt = des.CreateDecryptor();
            Byte[] baOutput = desdecrypt.TransformFinalBlock(baInput, 0, baInput.Length);
            return Encoding.Unicode.GetString(baOutput, 0, baOutput.Length);
        }

    }
}
