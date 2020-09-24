using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace P2
{
    class Program
    {
        public static byte[] ToByteArray(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static byte[] Combine(string password , string salt)
        {
            byte[] first = Encoding.ASCII.GetBytes(password);
            byte[] second = ToByteArray(salt);
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
        static string Hashing(byte[] byteArr, int numElements=5)
        {
            var provider = MD5.Create();
            byte[] bytes = provider.ComputeHash(byteArr);
            var selected = bytes.Take(numElements).ToArray();
            string computedHash = BitConverter.ToString(selected);
            return computedHash;
        }

        public static string AlphaNumeric(int strLen = 3)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string alphaNumeric = String.Concat(alphabet, alphabet.ToUpper(), numbers);
            var stringChar = new char[strLen];
            var random = new Random();
            for (int i = 0; i < stringChar.Length; i++)
            {
                stringChar[i] = alphaNumeric[random.Next(alphaNumeric.Length)];
            }

            var finalString = new String(stringChar);
            return finalString;

        }

        static void looper(string salt)
        {
            var numberNames = new Dictionary<string, string>();
            
            for (int i = 3; i < 12; i++)
            {
                for (int j = 0; j < 1000000; j++)
                {
                    var password = AlphaNumeric(i);
                    var combinedBytArr = Combine(password, salt);
                    string output = Hashing(combinedBytArr);
                    
                    if (numberNames.ContainsKey(output))
                    {
                        if (numberNames[output] != password)
                        {
                            Console.WriteLine(String.Concat(numberNames[output],',',password));
                            goto LoopEnd;
                        }
                    }
                    else
                    {
                        numberNames.Add(output, password);
                    }
                }
            }
            LoopEnd: 
            Console.Write("");
        }
        
        static void Main(string[] args)
        {
            string salt = args[0];
            looper(salt);

        }
    }
}