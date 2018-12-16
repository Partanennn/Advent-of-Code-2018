using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
/*
--- Day 4: Repose Record ---
You've sneaked into another supply closet - this time, it's across from the prototype suit manufacturing lab. You need to sneak inside and fix the issues with the suit, but there's a guard stationed outside the lab, so this is as close as you can safely get.

As you search the closet for anything that might help, you discover that you're not the first person to want to sneak in. Covering the walls, someone has spent an hour starting every midnight for the past few months secretly observing this guard post! They've been writing down the ID of the one guard on duty that night - the Elves seem to have decided that one guard was enough for the overnight shift - as well as when they fall asleep or wake up while at their post (your puzzle input).

For example, consider the following records, which have already been organized into chronological order:

[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up
Timestamps are written using year-month-day hour:minute format. The guard falling asleep or waking up is always the one whose shift most recently started. Because all asleep/awake times are during the midnight hour (00:00 - 00:59), only the minute portion (00 - 59) is relevant for those events.

Visually, these records show that the guards are asleep at these times:

Date   ID   Minute
            000000000011111111112222222222333333333344444444445555555555
            012345678901234567890123456789012345678901234567890123456789
11-01  #10  .....####################.....#########################.....
11-02  #99  ........................................##########..........
11-03  #10  ........................#####...............................
11-04  #99  ....................................##########..............
11-05  #99  .............................................##########.....
The columns are Date, which shows the month-day portion of the relevant day; ID, which shows the guard on duty that day; and Minute, which shows the minutes during which the guard was asleep within the midnight hour. (The Minute column's header shows the minute's ten's digit in the first row and the one's digit in the second row.) Awake is shown as ., and asleep is shown as #.

Note that guards count as asleep on the minute they fall asleep, and they count as awake on the minute they wake up. For example, because Guard #10 wakes up at 00:25 on 1518-11-01, minute 25 is marked as awake.

If you can figure out the guard most likely to be asleep at a specific time, you might be able to trick that guard into working tonight so you can have the best chance of sneaking in. You have two strategies for choosing the best guard/minute combination.

Strategy 1: Find the guard that has the most minutes asleep. What minute does that guard spend asleep the most?

In the example above, Guard #10 spent the most minutes asleep, a total of 50 minutes (20+25+5), while Guard #99 only slept for a total of 30 minutes (10+10+10). Guard #10 was asleep most during minute 24 (on two days, whereas any other minute the guard was asleep was only seen on one day).

While this example listed the entries in chronological order, your entries are in the order you found them. You'll need to organize them before they can be analyzed.

What is the ID of the guard you chose multiplied by the minute you chose? (In the above example, the answer would be 10 * 24 = 240.)
*/

namespace C__testing
{

    class Program
    {
        
        static void test() {
            StreamReader sr = new StreamReader("input.txt");
            String[] guards = new String[23];
            int guardIndex = 0;

            SortedDictionary<string, string> table = new SortedDictionary<string, string>();
            SortedDictionary<string, string> sortedTable = new SortedDictionary<string, string>();
            int i = 0;
            while(!sr.EndOfStream) {
                String line = sr.ReadLine();
                String key = "";
                Boolean end = false;
                int counter = 0;    // Counts for spaces
                
                // Converts string to timestamp that is key for table
                foreach(char a in line) {
 
                    if(Char.IsDigit(a) && !end) {
                        key += a;
                    } else if(a == ' ') {
                        counter++;
                    } else if(counter == 2) {
                        end = true;
                    }
                }

                // Debug to check that key is right
                //Console.WriteLine(key);

                // Adds line to SortedDictionary, where key is timestamp
                table[key] = line;
            }

            // Puts table data to sortedTable and data is sorted
            foreach(KeyValuePair<string, string> info in table) {
                sortedTable[info.Key] = info.Value;
            }


            // This is where are all guard ids' with minutes slept
            Dictionary<int, int> guardsMins = new Dictionary<int, int>();
            string guardID = "";
            int startMin = 0, endMin = 0;

            
            // This dictionary is to see which minute the guard is sleeping the most
            Dictionary<int, int> solution = new Dictionary<int, int>();
            for(int a = 0; a < 60; a++)
                solution[a] = 0;

            foreach(KeyValuePair<string, string> pair in sortedTable) {
                int time = 0;
                string hours = "", minutes = "";
                Boolean minute = false, isGuard = false;
                int counter = 0;                    // Counts for spaces
                string info = "";

                foreach(char a in pair.Value) {
                    if(a == '#') {
                        guardID = "";
                        isGuard = true;
                    } else if(Char.IsDigit(a) && isGuard)
                        guardID += a;
                    else if(Char.IsLetter(a))
                        info += a;
                }
                

                foreach(char a in pair.Value) {
                    if(a == ' ') {
                        counter++;
                    } else if(a == ':') {
                        minute = true;
                    } else if(minute && char.IsDigit(a) && counter == 1) {
                        minutes += a;
                    }
                }
                
                // Sets starting minute and ending minute of sleeping for a guard
                if(info == "fallsasleep")
                    startMin = Int32.Parse(minutes);
                else if(info == "wakesup") {
                    endMin = Int32.Parse(minutes)-1;
                    int slept = endMin - startMin;
                    if(slept < 0)
                        slept += 60;
                    if(guardsMins.ContainsKey(Int32.Parse(guardID)))
                        guardsMins[Int32.Parse(guardID)] += slept;
                    else
                        guardsMins[Int32.Parse(guardID)] = slept;
                }

                if(guardID == "1993") {
                    // Console.WriteLine(pair.Value);
                }
                if(guardID == "1993" && info == "fallsasleep") {
                    // Console.WriteLine("Sleep: {0}", startMin);
                    
                } else if(guardID == "1993" && info == "wakesup")  {
                    // Console.WriteLine("Awake: {0}, SLeep: {1}", endMin, startMin);
                    int minuutti = startMin;
                    //Console.Write("Alotus: {0}, ", minuutti);
                    for(minuutti = startMin; minuutti != (endMin-1); minuutti++) {
                        if(minuutti == 60)
                            minuutti = 0;
                        solution[minuutti]++;
                    }
                    // Console.WriteLine("Minuutti: {0}", minuutti);
                } else if(guardID == "1993" && info == "Guardbeginsshift") {
                    // Console.WriteLine("Starts shift");
                }

                // DEBUGGING: TO check that key and info are what tehy should be
                // Console.WriteLine("Guard id: {0}, Minutes: {1}, info: {2}", guardID, minutes, info);

            }
            
            
            // DEBUGGING: Check that guardsMins returns right keys and values
            // foreach(KeyValuePair<int, int> pair in guardsMins) {
            //     Console.WriteLine("Key: {0}, Value: {1}", pair.Key, pair.Value);
            // }


            foreach(KeyValuePair<int, int> a in solution) {
                Console.WriteLine("Key: {0}, Value: {1}", a.Key, a.Value);
            }

            // Gets max value a.k.a most minutes slept and guards id for that value
            int maxValue = solution.Max(KeyValuePair => KeyValuePair.Value);
            var maxKey = solution.Where(kvp => kvp.Value == maxValue).Select(kvp => kvp.Key).First();

            // SOLUTION
            Console.WriteLine("Minute spent most sleeping: {0}", maxKey);

            sr.Close();
        }

        static void Main(string[] args)
        {   
            test();
        }
    }
}
