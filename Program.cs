using System;
using System.IO;
using System.Collections.Specialized;

namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            String line = ""; 
            StreamReader sr = new StreamReader("input.txt");

            // These 2 ints counts duples and triples
            int duples = 0;
            int triples = 0;
            
            while(!sr.EndOfStream) {

                Boolean two = false;
                Boolean three = false;
                NameValueCollection letters = new NameValueCollection();
                line = sr.ReadLine();
                
                // Adds letters to collection and adds string "1" to its value,
                // if letter allready exists then it just adds "1" to it so if
                // the line have 3 a's, a will then have value of "1, 1, 1"
                foreach(char letter in line) {
                    int count = letters.Count;
                    letters.Add(letter.ToString(), "1");
                }

                // Changes all "1, 1, ,1 "" values to real numbers, but they
                // still are strings
                foreach(String key in letters.AllKeys) {
                    String value = letters[key];
                    int realValue = 0;
                    foreach(char temp in value) {
                        if(temp == '1') {
                            realValue++;
                        }
                    }
                    letters[key] = realValue.ToString();
                }
                
                // This checks if line have any duples or triples, when it
                // founds first duple it won't add any duples to global count,
                // same thing for triples. If it have found both it will stop
                // the loop
                for(int i = 0; i < letters.Count || (!two && !three); i++) {
                    if(Int32.Parse(letters.Get(i)) == 2 && !two) {
                        duples++;
                        two = true;
                    } else if(Int32.Parse(letters.Get(i)) == 3 && !three) {
                        triples++;
                        three = true;
                    }
                }
            }

            // This prints number of duples and triples to console
            Console.WriteLine("Duples: {0}, Triples: {1}", duples, triples);
            sr.Close(); // Close StreamReader
        }
    }
}
