using System;
using Util;

namespace POW
{
    class Program
    {
        static void Main(string[] args)
        {
            // a more optmized random function that could be used for potentially data mining
            for (int i = 0; i < 100000; i++) {
                string str = RandomString.Generate();
                Console.WriteLine(str);
            }
        }
    }
}
