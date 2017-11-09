using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace eContract.Common
{
    /// <summary>
    /// Summary description for Encryption.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// An instance of RsaProvider.
        /// </summary>
        public static RSACryptoServiceProvider RsaProvider; 
        private static string id = "xxxx";
        private static readonly int keySize = 128;
        private static readonly int blockSize = 86;
        private const string xmlKey = @"<RSAKeyValue><Modulus>1O+d9ECJC89ICkaB9OX2VbBQ2M4mXHM4iD4PTqHBdSaN0JtWAqu1hqR5J8R80r8/bROCj8ly/+JvjEOunoaFs457s9Ud9dffBlkxg8GpfdAwu7sfqkdLy5EyyB/v45tF3SRWEaHOBUuBnnsNd+efoEfGQo1IbTtvOYsaVm+PooU=</Modulus><Exponent>AQAB</Exponent><P>+kEvU4arpWL/cv2Q88QWmcsAsSifr/Vf5wLhjDDWIYU3qVDtqlYOL/IMqfXCms/V9oOENQw0bM2G69kqkixlhQ==</P><Q>2dMZsAdDl54h0sxeec0jaZDfy0NlHW6DLMlRB3z4vEd+0TorBb4/IwxG49i0M/dFdim0ZblsDno0SXWYJThZAQ==</Q><DP>hge7bSTHcYCgB9o+dBAleqD68ecr/3WPs44bdpYBWVqcARbS81O7rXoZxj1VyMxfb/PoLvEmcs9w34gkAL2+cQ==</DP><DQ>dhWCr3LAqckH/QbdkJoswGXRbJe0kSf/5J+eVbjh/u+jSDmIaSyhfZaCN7KavjEmbtBdA2hps8972Pbu6/6IAQ==</DQ><InverseQ>yZ/nLFBAoHqjaX0A/1VkbeT9idmWOj9VMwLrKmYOdAkPGfJAs5jdBeN6Xg6zSD1t6NKCqtATCjcYi8QCQXZM8g==</InverseQ><D>EQMj54PQbzUcWFXRxDMrPyVbEDdIMVKzTY9HwcyCnE18PxJqCMSXOC6jz12Pa3cEJj7My5gYrAD3UImHxfqRa+WspYggraZp3Ad02xDs713KE8oqqCnZMWXzcDdIgEezCje+k60cXWppcu8JfZjyk4q9sWHCuYEQprNMFH0+UAE=</D></RSAKeyValue>";

        static Encryption()
        {
            CspParameters CSPParam = new CspParameters();
            CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
            RsaProvider = new RSACryptoServiceProvider(CSPParam);
            RsaProvider.FromXmlString(xmlKey);
        }

        private static string internalEncrypt(string data)
        {
            if (data == null)
                return null;

            byte[] buf = System.Text.Encoding.UTF8.GetBytes(data);
            int left = buf.Length;
            byte[] result = new byte[((left - 1) / blockSize + 1) * keySize];
            int done = 0;

            while (left > 0)
            {
                byte[] tmp;
                if (buf.Length <= blockSize)
                    tmp = buf;
                else
                {
                    int size = (blockSize < left) ? blockSize : left;
                    tmp = new byte[size];
                    Array.Copy(buf, buf.Length - left, tmp, 0, size);
                }

                byte[] encrypted;
                try
                {
                    encrypted = RsaProvider.Encrypt(tmp, false);
                }
                catch
                {
                    //K2BLL.SystemFrameWork.EventsLog.WriteEvent("Encryption","Error during encryption");
                    return null;
                }

                Array.Copy(encrypted, 0, result, done, encrypted.Length);
                left -= tmp.Length;
                done += encrypted.Length;
            }


            return Convert.ToBase64String(result);
        }

        private static string internalDecrypt(string data)
        {
            if (data == null)
                return null;

            byte[] buf = Convert.FromBase64String(data);

            if (null == buf || buf.Length % keySize != 0)
            {
                //	K2BLL.SystemFrameWork.EventsLog.WriteEvent("Encryption","Data string cannot be decrypted or Bad size");
                return null;
            }

            byte[] result = new byte[buf.Length / keySize * blockSize];
            byte[] tmp = new byte[keySize];
            int done = 0;
            for (int i = 0; i < buf.Length / keySize; i++)
            {
                Array.Copy(buf, i * keySize, tmp, 0, keySize);

                byte[] decrypted;
                try
                {
                    //DebugTracer.WriteTrace(true,data);
                    decrypted = RsaProvider.Decrypt(tmp, false);
                }
                catch 
                {
                    //K2BLL.SystemFrameWork.EventsLog.WriteEvent("Encryption","Error during decryption");
                    return null;
                }

                Array.Copy(decrypted, 0, result, done, decrypted.Length);
                done += decrypted.Length;
            }

            return System.Text.Encoding.UTF8.GetString(result, 0, done);
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="data">the input string</param>
        /// <param name="id">The verify string id. Pass null if don't care.</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string data)
        {
            try
            {
                return internalEncrypt(Encryption.id + "," + (DateTime.Now.Ticks / 10000000) + "," + data);
            }
            catch// please handle(Exception ex)
            {
                //K2BLL.SystemFrameWork.EventsLog.WriteEvent("Encryption", "Encrypt string generate errors");
                return null;
            }
        }

        /// <summary>
        /// Descrypt a string
        /// </summary>
        /// <param name="data">The string to decrypt</param>
        /// <returns>the decrypted string</returns>
        public static string Decrypt(string data)
        {
            return Decrypt(data, 0);
        }

        /// <summary>
        /// Decrypt a string with some validation
        /// </summary>
        /// <param name="data">The string to decrypt</param>
        /// <param name="id">The verify string id. Pass null if don't care</param>
        /// <param name="timeWindow">The time window that make sure the encrypt string expired after this time window. 
        /// The unit is second.
        /// </param>
        /// <returns>decrypted string</returns>
        public static string Decrypt(string data, int timeWindow)
        {
            try
            {
                string result = internalDecrypt(data);
                if (result == null)
                    return null;

                int index = result.IndexOf(',');
                if (index < 0)
                    return null;
                string tmp = result.Substring(0, index);
                if (Encryption.id != tmp)
                    return null;

                index++;
                int index2 = result.IndexOf(',', index);
                string temp = result.Substring(index, index2 - index);
                if (index2 < 0 || (timeWindow > 0 && DateTime.Now.Ticks / 10000000 - Int32.Parse(result.Substring(index + 1, index2 - index)) > timeWindow))
                    return null;

                return result.Substring(index2 + 1);
            }
            catch
            { 
                return null;
            }
        }
    }
}
