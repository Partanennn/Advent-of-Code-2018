using System;
using System.IO;
using System.Collections.Generic;

/*
--- Day 5: Alchemical Reduction ---
You've managed to sneak in to the prototype suit manufacturing lab. The Elves are making decent progress, but are still struggling with the suit's size reduction capabilities.

While the very latest in 1518 alchemical technology might have solved their problem eventually, you can do better. You scan the chemical composition of the suit's material and discover that it is formed by extremely long polymers (one of which is available as your puzzle input).

The polymer is formed by smaller units which, when triggered, react with each other such that two adjacent units of the same type and opposite polarity are destroyed. Units' types are represented by letters; units' polarity is represented by capitalization. For instance, r and R are units with the same type but opposite polarity, whereas r and s are entirely different types and do not react.

For example:

In aA, a and A react, leaving nothing behind.
In abBA, bB destroys itself, leaving aA. As above, this then destroys itself, leaving nothing.
In abAB, no two adjacent units are of the same type, and so nothing happens.
In aabAAB, even though aa and AA are of the same type, their polarities match, and so nothing happens.
Now, consider a larger example, dabAcCaCBAcCcaDA:

dabAcCaCBAcCcaDA  The first 'cC' is removed.
dabAaCBAcCcaDA    This creates 'Aa', which is removed.
dabCBAcCcaDA      Either 'cC' or 'Cc' are removed (the result is the same).
dabCBAcaDA        No further actions can be taken.
After all possible reactions, the resulting polymer contains 10 units.

How many units remain after fully reacting the polymer you scanned? (Note: in this puzzle and others, the input is large; if you copy/paste your input, make sure you get the whole thing.)--- Day 5: Alchemical Reduction ---
You've managed to sneak in to the prototype suit manufacturing lab. The Elves are making decent progress, but are still struggling with the suit's size reduction capabilities.

While the very latest in 1518 alchemical technology might have solved their problem eventually, you can do better. You scan the chemical composition of the suit's material and discover that it is formed by extremely long polymers (one of which is available as your puzzle input).

The polymer is formed by smaller units which, when triggered, react with each other such that two adjacent units of the same type and opposite polarity are destroyed. Units' types are represented by letters; units' polarity is represented by capitalization. For instance, r and R are units with the same type but opposite polarity, whereas r and s are entirely different types and do not react.

For example:

In aA, a and A react, leaving nothing behind.
In abBA, bB destroys itself, leaving aA. As above, this then destroys itself, leaving nothing.
In abAB, no two adjacent units are of the same type, and so nothing happens.
In aabAAB, even though aa and AA are of the same type, their polarities match, and so nothing happens.
Now, consider a larger example, dabAcCaCBAcCcaDA:

dabAcCaCBAcCcaDA  The first 'cC' is removed.
dabAaCBAcCcaDA    This creates 'Aa', which is removed.
dabCBAcCcaDA      Either 'cC' or 'Cc' are removed (the result is the same).
dabCBAcaDA        No further actions can be taken.
After all possible reactions, the resulting polymer contains 10 units.

How many units remain after fully reacting the polymer you scanned? (Note: in this puzzle and others, the input is large; if you copy/paste your input, make sure you get the whole thing.)
*/
namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            // DEBUGGIN AND TESTING
            // StreamReader sr = new StreamReader("test.txt");
            while(!sr.EndOfStream) {
                string line = sr.ReadLine();
                Remover(line);
            }
            sr.Close();
        }

        static void Remover(string line) {
            int units = 0;
            int charBig = 65, charSmall = 97;
            var list = new List<int>();

            for(; charBig != 91; charBig++, charSmall++) {
                string temp = line;
                
                // DEBUGGING:
                // Console.WriteLine("-------------------------------");
                for(int i = 0; i < temp.Length; i++) {

                    // DEBUGGING:
                    // if(i == 0) {
                    //     Console.WriteLine("Temp before remove: "+temp);
                    // }
                    // DEBUGGING:
                    // Console.Write(" {0} ", temp[i]);

                    if(temp[i] == (char)charBig || temp[i] == (char)charSmall) {
                        // DEBUGGING:
                        // Console.Write("Removed, index: {1}////", temp[i], i);
                        temp = temp.Remove(i, 1);
                        i--;
                    }
                }
                // DEBUGGING:
                // Console.WriteLine();
                // Console.WriteLine("Temp before reactor: "+temp);
                
                temp = Reactor(temp);
                
                // DEBUGGING
                // Console.WriteLine("Temp: {0}, Length: {1}", temp, temp.Length);
                list.Add(temp.Length);
            }

            // Goes throw arraylist which is full of string lengths
            // and sets min to be the lowest
            int min = 13000;
            foreach (var type in list) {
                if (type < min) {
                    min = type;
                }
            }

            // Prints the smallest value in arraylist, which is answer to question
            Console.WriteLine("Answer: {0}", min);
        }

        static string Reactor(string line) {
            
            int changes = 1;
            
            // Prints length of line at start
            // Console.WriteLine(line.Length);
            

            // This while runs as long as there are any changes for line,
            // if there are'nt any changes then this while will end
            while(changes != 0) {
                changes = 1;
                for(int i = 0; i < line.Length-1; i++) {
                    // If character and the next character are, for example a and A or A and a, then this if happens
                    if((int)line[i]+32 == (int)line[i+1] || (int)line[i] == (int)line[i+1]-32 || (int)line[i+1]+32 == (int)line[i] || (int)line[i]-32 == (int)line[i+1]) {
                        line = line.Remove(i, 2);
                        changes++;
                    }
            
                    if(i == line.Length-2 && changes == 1) {
                        changes = 0;
                    }
                }
            }
            // Gives solutions, units
            // Console.WriteLine("Units: {0}, Chars: {1} {2}", line.Length, a, b);
            return line;
        }
    }
}
