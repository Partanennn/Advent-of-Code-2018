using System;
using System.IO;
/* 
--- Day 3: No Matter How You Slice It ---
The Elves managed to locate the chimney-squeeze prototype fabric for Santa's suit (thanks to someone who helpfully wrote its box IDs on the wall of the warehouse in the middle of the night). Unfortunately, anomalies are still affecting them - nobody can even agree on how to cut the fabric.

The whole piece of fabric they're working on is a very large square - at least 1000 inches on each side.

Each Elf has made a claim about which area of fabric would be ideal for Santa's suit. All claims have an ID and consist of a single rectangle with edges parallel to the edges of the fabric. Each claim's rectangle is defined as follows:

The number of inches between the left edge of the fabric and the left edge of the rectangle.
The number of inches between the top edge of the fabric and the top edge of the rectangle.
The width of the rectangle in inches.
The height of the rectangle in inches.
A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches from the left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall. Visually, it claims the square inches of fabric represented by # (and ignores the square inches of fabric represented by .) in the diagram below:

...........
...........
...#####...
...#####...
...#####...
...#####...
...........
...........
...........
The problem is that many of the claims overlap, causing two or more claims to cover part of the same areas. For example, consider the following claims:

#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
Visually, these claim the following areas:

........
...2222.
...2222.
.11XX22.
.11XX22.
.111133.
.111133.
........
The four square inches marked with X are claimed by both 1 and 2. (Claim 3, while adjacent to the others, does not overlap either of them.)

If the Elves all proceed with their own plans, none of them will have enough fabric. How many square inches of fabric are within two or more claims?
*/

namespace C__testing
{
    class Program
    {
        static void test() {
            StreamReader sr = new StreamReader("input.txt");

            int x = 1337;
            int y = 1337;
            String[,] fabric = new String[x, y];
            
            // Creates dots for all table values
            for(int i = 0; i < x; i++) {
                for(int j = 0; j < y; j++) {
                    fabric[i, j] = ".";
                }
            }
            
            while(!sr.EndOfStream) {
                String info = sr.ReadLine();
                
                String temp = "";
                String id = "", coordinate_x = "", coordinate_y = "", width = "", height = "";
                bool loc_separator = false, size_separator = false;
                
                foreach(char c in info) {
                    bool number = Char.IsDigit(c);
                    if(c == '#') {
                        temp = "id";
                    } else if(c == '@') {
                        temp = "loc";
                    } else if(c == ':') {
                        temp = "size";
                        loc_separator = false;
                    } else if(c == ',') {
                        loc_separator = true;
                    } else if(c == 'x') {
                        size_separator = true;
                    }

                    if(temp == "id" && number) {
                        id += c;
                    } else if(temp == "loc" && number) {
                        if(!loc_separator) {
                            coordinate_x += c;
                        } else {
                            coordinate_y += c;
                        }
                    } else if(temp == "size" && number) {
                        if(!size_separator) {
                            width += c;
                        } else {
                            height += c;
                        }
                    }


                }

                int coor_x = Int32.Parse(coordinate_x); 
                int coor_y = Int32.Parse(coordinate_y);
                int w = Int32.Parse(width);
                int h = Int32.Parse(height);
 
                for(int i = 0; i < w; i++) {
                    for(int j = 0; j < h; j++) {
                        if(fabric[coor_x+i, coor_y+j] == ".")
                            fabric[coor_x+i, coor_y+j] = id;
                        else 
                            fabric[coor_x+i, coor_y+j] = "X";
                    }
                }

                // Draws 
                // Console.WriteLine("ID: {0}, coordinates: {1}, {2}  size: {3} x {4}", id, coordinate_x, coordinate_y, width, height);

            }
            sr.Close();
            StreamReader st = new StreamReader("Input.txt");

            while(!st.EndOfStream) {
                String line = st.ReadLine();
                Boolean is_id = false, is_size = false, size_change = false;
                String id2 = "", size_x = "", size_y = "";
                int real_size = 0, should_size = 0;

                foreach(char a in line) {
                    if(a == '#') {
                        is_id = true;
                    } else if(a == ' ')  {
                        is_id = false;
                    } else if(a == ':') {
                        is_size = true;
                    } else if(a == 'x') {
                        size_change = true;
                    }

                    if(is_id && Char.IsDigit(a)) {
                        id2 += a;
                    } else if(is_size && Char.IsDigit(a) && !size_change) {
                        size_x += a;
                    } else if(is_size && Char.IsDigit(a) && size_change) {
                        size_y += a;
                    }
                }

                for(int i = 0; i < x; i++) {
                    for(int j = 0; j < y; j++) {
                        if(fabric[i, j] == id2) {
                            real_size++;
                        }
                    }
                }

                should_size = Int32.Parse(size_x) * Int32.Parse(size_y);

                if(should_size == real_size) {
                    Console.WriteLine("No overlapping ID: {0}", id2);
                }
            }
            st.Close();
        }

        static void Main(string[] args)
        {
                
            test();
        }
    }
}