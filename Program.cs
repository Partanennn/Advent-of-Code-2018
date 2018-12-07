using System;

namespace C__testing
{
    class Program
    {
        static void Main(string[] args)
        {
            String info = "#1 @ 1,3: 4x4";
            int x = 10;
            int y = 10;
            char id;
            int coordinate_x, coordinate_y, width, height;
            String[,] fabric = new String[x, y];

            for(int i = 0; i < x; i++) {
                for(int j = 0; j < y; j++) {
                    fabric[i, j] = ".";
                    //Console.WriteLine(fabric[i, j]);
                }
            }
            id = ' ';
            foreach(char c in info) {
                if(c == '#') {
                    id = c;
                } else if(id != )
            }

            for(int i = 0; i < x; i++) {
                for(int j = 0; j < y; j++) {
                    Console.Write(fabric[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}