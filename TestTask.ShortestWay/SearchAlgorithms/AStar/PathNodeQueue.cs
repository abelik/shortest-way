using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.ShortestWay.SearchAlgorithms.AStar
{
    public class PathNodeQueue
    {
        private readonly List<PathNode> _items = new List<PathNode>();

        public int Count => _items.Count;

        public void Enqueue(PathNode item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
            _items.Sort((a, b) => a.PathLength.CompareTo(b.PathLength));
        }

        public PathNode Dequeue()
        {
            if (!_items.Any())
                throw new ArgumentOutOfRangeException();

            var first = _items.First();                
            _items.RemoveAt(0);
            return first;
        }
    }    
}