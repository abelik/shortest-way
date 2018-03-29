using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestTask.ShortestWay.SearchAlgorithms.AStar
{
    // https://en.wikipedia.org/wiki/A*_search_algorithm

    public class AStarSearchAlgorithm : ISearchAlgorithm
    {
        private const int MaximumCellValueOnTheMap = 100;
        private const int ObstacleCellValueOnTheMap = 0;

        private readonly Dictionary<Location, Location> _cameFromTo = new Dictionary<Location, Location>();
        private readonly Dictionary<Location, double> _costSoFar = new Dictionary<Location, double>();        
           
        public Location[] FindShortestWay(World.Cell[,] cells, Location start, Location goal)
        {
            var open = new PathNodeQueue();
            var pathNode = new PathNode()
            {
                Location = start,
                PathLength = cells[start.X, start.Y].Passability                
            };

            open.Enqueue(pathNode);
            
            _cameFromTo[start] = pathNode.Location;

            _costSoFar[start] = pathNode.PathLength;

            while (open.Count > 0)
            {
                var current = open.Dequeue();

                if (current.Location.Equals(goal))
                    break;

                foreach (var next in GetNeighbours(cells, current.Location))
                {
                    var newCost = _costSoFar[current.Location] + Cost(cells, next);

                    if (_costSoFar.ContainsKey(next) && (newCost > _costSoFar[next]))
                        continue;

                    _costSoFar[next] = newCost;

                    open.Enqueue(
                        new PathNode()
                        {
                            Location = next,
                            PathLength = newCost + Heuristic(next, goal)
                        });
                   
                    _cameFromTo[next] = current.Location;
                }
            }

            return GetPath(start, goal);
        }        

        private static double Heuristic(Location a, Location b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private static IEnumerable<Location> GetNeighbours(World.Cell[,] cells, Location point)
        {
            var result = new Collection<Location>();
            var neighbourPoints = new Location[4];

            neighbourPoints[0] = new Location(point.X - 1, point.Y);
            neighbourPoints[1] = new Location(point.X, point.Y - 1);
            neighbourPoints[2] = new Location(point.X, point.Y + 1);
            neighbourPoints[3] = new Location(point.X + 1, point.Y);

            foreach (var npoint in neighbourPoints)
            {
                if (npoint.X < 0 || npoint.X >= cells.GetLength(0))
                    continue;
                if (npoint.Y < 0 || npoint.Y >= cells.GetLength(1))
                    continue;
                if (cells[npoint.X, npoint.Y].Passability == ObstacleCellValueOnTheMap)
                    continue;

                result.Add(npoint);
            }

            return result;
        }

        private static double Cost(World.Cell[,] cells, Location next)
        {
            return Math.Abs(cells[next.X, next.Y].Passability - MaximumCellValueOnTheMap);
        }

        private Location[] GetPath(Location start, Location goal)
        {
            var current = goal;
            var path = new List<Location> { goal };

            if (_cameFromTo.ContainsKey(start) && _cameFromTo.ContainsKey(goal))
            {
                while (current != start)
                {
                    if (!_cameFromTo.ContainsKey(current))
                        continue;

                    current = _cameFromTo[current];
                    path.Add(current);
                }
            }

            path.Reverse();

            return path.ToArray();
        }
    }
}
