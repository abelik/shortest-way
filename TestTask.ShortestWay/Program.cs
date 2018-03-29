using System;
using System.Linq;

namespace TestTask.ShortestWay
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 30;
            const int height = 30;

            var world = new World(width, height);

            var resultFindShortestWay = world.FindShortestWay(new Location(0, 0), new Location(width - 1, height - 1));            

            for (var i = 0; i < world.Cells.GetLength(0); i++)
            {                
                for (var j = 0; j < world.Cells.GetLength(1); j++)
                {
                    var value = world.Cells[i, j].Passability.ToString().PadLeft(3, '0');
                    if (resultFindShortestWay != null && resultFindShortestWay.Any(o => o.X == i && o.Y == j))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        value = $"[{value}]";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        value = $" {value} ";
                    }

                    Console.Write($"{value}");
                }
                Console.WriteLine("");
            }            

            Console.ReadLine();
        }
    }
}
