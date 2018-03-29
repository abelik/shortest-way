using System;
using TestTask.ShortestWay.SearchAlgorithms;
using TestTask.ShortestWay.SearchAlgorithms.AStar;

namespace TestTask.ShortestWay
{
    public class World
    {
        public class Cell : Location
        {
            // Defines cell passability from 0 (can't go) to 100 (normal passability)
            // The higher is passability, the quicker it is possible to pass the cell
            public byte Passability; 

            public Cell(int x, int y, byte passability)
                : base(x, y)
            {
                this.Passability = passability;
            }
        }

        private Cell[,] cells; // World map

        public World(int sizeX, int sizeY)
        {
            var rnd = new Random();

            // Build map and randomly set passability for each cell
            cells = new Cell[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
                for (int y = 0; y < sizeY; y++)
                    cells[x, y] = new Cell(x, y, (byte) rnd.Next(0, 100));
        }

        public Cell[,] Cells => cells;

        public ISearchAlgorithm SearchAlgorithm { get; set; } = new AStarSearchAlgorithm();

        public Location[] FindShortestWay(Location startLoc, Location endLoc)
        {            
            return SearchAlgorithm.FindShortestWay(cells, startLoc, endLoc);;
        }
    }
}
