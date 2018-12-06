using System;
using System.IO;
using System.Collections;

namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            int freq = 0;
            ArrayList taulu = new ArrayList();
            Boolean duplicate = false;
            while(!sr.EndOfStream) {
                String line = sr.ReadLine();
                freq += Int32.Parse(line);
                taulu.Add(freq);

            }
            sr.Close();

            ArrayList seen = new ArrayList();
            
            while(!duplicate) {
                foreach(int frequency in taulu) {
                    foreach(int x in seen) {
                        if(frequency == x) {
                            Console.WriteLine(frequency);
                            duplicate = true;
                        } else {
                            seen.Add(frequency);
                        }
                    }
                }
            }
        }
    }
}
