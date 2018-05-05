using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleForStatisticsEnvironment.GraphGenerator
{
    public class Topology
    {
        public List<Tuple<int, int>> Neighbourhoods { get; set; }
        public Dictionary<int, int> States { get; set; }

        public Topology()
        {
            Neighbourhoods = new List<Tuple<int, int>>();
        }

        public int DegreeOf(int i)
        {
            return Neighbourhoods.Count(t => t.Item1 == i || t.Item2 == i);
        }

        public bool AreNeighbours(int n1, int n2)
        {
            return Neighbourhoods.Any(t => t.Item1 == n1 && t.Item2 == n2) || Neighbourhoods.Any(t => t.Item1 == n2 && t.Item2 == n1);
        }
    }
}
