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
            // StreamReader sr = new StreamReader("input.txt");
            StreamReader sr = new StreamReader("test.txt");
            int c = 65;
            int maxX = 0, maxY = 0;

            while(!sr.EndOfStream) {
                string coordinates = sr.ReadLine();
                Boolean dot = false;
                string coordA = "", coordB = "";
                
                foreach(char a in coordinates) {
                    if(Char.IsDigit(a) && !dot) {
                        coordA += a;
                    } else if(Char.IsDigit(a) && dot) {
                        coordB += a;
                    } else dot = true;
                }

                if(Int32.Parse(coordA) > maxX) maxX = Int32.Parse(coordA);
                if(Int32.Parse(coordB) > maxY) maxY = Int32.Parse(coordB);
            }

            // Console.WriteLine("MAX x: {0}, MAX y: {1}", maxX, maxY);

            maxX+=2;
            maxY++;
            string[,] coords = new string[maxX, maxY];

            for(int i = 0; i < maxX; i++) {
                for(int j = 0; j < maxY; j++) {
                    coords[i, j] = ".";
                }
            }
            sr.Close();
            sr = new StreamReader("test.txt");
            while(!sr.EndOfStream) {
                string coordinates = sr.ReadLine();
                Boolean dot = false;
                string coordA = "", coordB = "";
                
                foreach(char a in coordinates) {
                    if(Char.IsDigit(a) && !dot) {
                        coordA += a;
                    } else if(Char.IsDigit(a) && dot) {
                        coordB += a;
                    } else dot = true;
                }
                
                // Console.WriteLine("CoordA: {0}, CoordB: {1}, MAX x: {2}, MAX y: {3}", coordA, coordB, maxX, maxY);
                coords[Int32.Parse(coordB), Int32.Parse(coordA)] = ((char)c).ToString();
                c++;
            }

            for(int i = 0; i < maxX; i++) {
                for(int j = 0; j < maxY; j++) {
                    Console.Write(coords[i, j]);
                }
                Console.WriteLine();
            }    

            sr.Close();
        }
    }
}
