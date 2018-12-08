using System;
using System.IO;
using System.Collections.Specialized;

/*
--- Day 2: Inventory Management System ---
You stop falling through time, catch your breath, and check the screen on the device. "Destination reached. Current Year: 1518. Current Location: North Pole Utility Closet 83N10." You made it! Now, to find those anomalies.

Outside the utility closet, you hear footsteps and a voice. "...I'm not sure either. But now that so many people have chimneys, maybe he could sneak in that way?" Another voice responds, "Actually, we've been working on a new kind of suit that would let him fit through tight spaces like that. But, I heard that a few days ago, they lost the prototype fabric, the design plans, everything! Nobody on the team can even seem to remember important details of the project!"

"Wouldn't they have had enough fabric to fill several boxes in the warehouse? They'd be stored together, so the box IDs should be similar. Too bad it would take forever to search the warehouse for two similar box IDs..." They walk too far away to hear any more.

Late at night, you sneak to the warehouse - who knows what kinds of paradoxes you could cause if you were discovered - and use your fancy wrist device to quickly scan every box and produce a list of the likely candidates (your puzzle input).

To make sure you didn't miss any, you scan the likely candidate boxes again, counting the number that have an ID containing exactly two of any letter and then separately counting those with exactly three of any letter. You can multiply those two counts together to get a rudimentary checksum and compare it to what your device predicts.

For example, if you see the following box IDs:

abcdef contains no letters that appear exactly two or three times.
bababc contains two a and three b, so it counts for both.
abbcde contains two b, but no letter appears exactly three times.
abcccd contains three c, but no letter appears exactly two times.
aabcdd contains two a and two d, but it only counts once.
abcdee contains two e.
ababab contains three a and three b, but it only counts once.
Of these box IDs, four of them contain a letter which appears exactly twice, and three of them contain a letter which appears exactly three times. Multiplying these together produces a checksum of 4 * 3 = 12.

What is the checksum for your list of box IDs?
*/

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
