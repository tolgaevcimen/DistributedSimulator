using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;

namespace ASyncSimulator1
{
    class Program
    {
        static void Main ( string[] args )
        {
            var edgeHolder = new EdgeHolder();
            var nodeHolder = new NodeHolder();
            var graphGenerator = GraphFactory.GetGraphGenerator(GraphType.Line);

            graphGenerator.Generate(50, nodeHolder, edgeHolder, "TurauMDS");

            var firstNode = nodeHolder.GetMinimumNode();
            if (firstNode == null) return;
            
            firstNode.UserDefined_SingleInitiatorProcedure(firstNode);

            Console.ReadKey();
        }
    }
}
