using System;
using System.IO;

namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            int answer = 0;
            while(!sr.EndOfStream) {
                String line = sr.ReadLine();

                answer += Int32.Parse(line);
                Console.WriteLine(answer);
            }
            sr.Close();
        }
    }
}
