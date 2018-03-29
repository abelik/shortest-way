namespace TestTask.ShortestWay.SearchAlgorithms
{
    public interface ISearchAlgorithm
    {
        Location[] FindShortestWay(World.Cell[,] cells, Location start, Location goal);
    }
}