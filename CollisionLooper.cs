using System;
using System.Collections.Generic;

namespace P2
{
    public class CollisionLooper
    {
        public static void Run(string salt, double numtry)
        {
            // initialize dictionary d[output]= input. A collision happens when 2 inputs map to the same output
            var numberNames = new Dictionary<string, string>();

            //outer loop determines the size of the randomly generated alphanumeric while inner loop 
            // determines number of tries given that character size to find a collision
            for (int i = 3; i < 12; i++)
            {
                for (int j = 0; j < numtry; j++)
                {
                    //create random alpha-numeric password with length i
                    var password = Transform.AlphaNumeric(i);
                    //create add salt to random alpha-numeric password with length i
                    var combinedBytArr = Transform.PasswordSalt(password, salt);
                    //Hash the password+salt byte array
                    string output = Transform.Hashing(combinedBytArr);

                    //Adding to dictionary d[output]=input, when there is inouts that create same output we
                    // return the 2 strings from which the collision occured 
                    if (numberNames.ContainsKey(output))
                    {
                        if (numberNames[output] != password)
                        {
                            Console.WriteLine(String.Concat(numberNames[output], ',', password));
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
    }
}