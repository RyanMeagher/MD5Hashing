using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace P2
{
    public class Transform
    {
        public static byte[] ToByteArray(String hexString)
        {
            // given a string of bytes in hex format with a space in between give the appropriate 
            // conversion to a byte array.
            int numberChars = hexString.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            
            return bytes;
        }
        
        public static byte[] PasswordSalt(string password, string salt)
        {
            //used to append salt onto password before hashing, normally there would be a user specific key of a 
            // certain number of bits to prevent against birthday attacks and to make the search space larger 
            // to generate collisions
            byte[] first = Encoding.ASCII.GetBytes(password);
            byte[] second = ToByteArray(salt);
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        
        public static string Hashing(byte[] byteArr, int numElements = 5)
        {
            //Using MD5 to hash only the first 5 elements. 
            var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(byteArr);
            var selected = bytes.Take(numElements).ToArray();
            string computedHash = BitConverter.ToString(selected);
            return computedHash;
        }
        
        public static string AlphaNumeric(int strLen = 3)
        {
            //creates a random alpha-numeric string length  defined by user
            // with a default size of 3, this will be used in birthday attacks when we want to 
            // find 2 different outputs that map to the same input
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string alphaNumeric = String.Concat(alphabet, alphabet.ToUpper(), numbers);

            //initialize stingChar used to create random characters in char arrays of desired size
            var stringChar = new char[strLen];
            var random = new Random();
            for (int i = 0; i < stringChar.Length; i++)
            {
                stringChar[i] = alphaNumeric[random.Next(alphaNumeric.Length)];
            }

            // convert character array to string 
            var finalString = new String(stringChar);
            return finalString;
        }
    }
    
    
    
    
}