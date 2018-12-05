using System;
using System.IO;

namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");

            while(!sr.EndOfStream) {
                Console.WriteLine(sr.ReadLine());
            }
                
            sr.Close();
        }
    }
}
