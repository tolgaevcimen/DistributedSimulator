using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            var algorithms = new List<string>(new[] { "GoddardMDS_rand", "ChiuDS_rand", "GoddardMDS_rand", "TurauMDS_rand" });
            var graphTypes = new List<GraphType>(new[] { GraphType.Line, GraphType.BinaryTree, GraphType.Star, GraphType.Circle, GraphType.Complete, GraphType.Random });

            foreach (var algorithm in algorithms)
            {
                foreach (var graphType in graphTypes)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            var edgeHolder = new EdgeHolder();
                            var nodeHolder = new NodeHolder();
                            var graphGenerator = GraphFactory.GetGraphGenerator(graphType);

                            var nodeCount = i * 10;

                            graphGenerator.Generate(nodeCount, nodeHolder, edgeHolder, algorithm);

                            var beforeInNodes = string.Join(", ", nodeHolder.GetCopyList().Where(n => n.Selected()).Select(n => n.Id));
                            var beforeNInNodes = string.Join(", ", nodeHolder.GetCopyList().Where(n => !n.Selected()).Select(n => n.Id));

                            Console.WriteLine("before in nodes:\t{0}", beforeInNodes);
                            Console.WriteLine("before n-in nodes:\t{0}", beforeNInNodes);

                            var firstNode = nodeHolder.GetMinimumNode();
                            if (firstNode == null) return;

                            firstNode.UserDefined_SingleInitiatorProcedure(firstNode);

                            Console.ReadKey();

                            var afterInNodes = string.Join(", ", nodeHolder.GetCopyList().Where(n => n.Selected()).Select(n => n.Id));
                            var afterNInNodes = string.Join(", ", nodeHolder.GetCopyList().Where(n => !n.Selected()).Select(n => n.Id));

                            Console.WriteLine("after in nodes:\t{0}", afterInNodes);
                            Console.WriteLine("after n-in nodes:\t{0}", afterNInNodes);

                            var eachNodeMessageCount = string.Join(", ", nodeHolder.GetCopyList().Select(n => string.Format("({0}:{1})", n.Id, n.MessageCount)));
                            var totalMessageCount = nodeHolder.GetCopyList().Sum(n => n.MessageCount).ToString();
                            Console.WriteLine("eachNodeMessageCount: {0}", eachNodeMessageCount);
                            Console.WriteLine("totalMessageCount: {0}", totalMessageCount);

                            var eachNodeMoveCount = string.Join(", ", nodeHolder.GetCopyList().Select(n => string.Format("({0}:{1})", n.Id, n.MoveCount)));
                            var totalMoveCount = nodeHolder.GetCopyList().Sum(n => n.MoveCount).ToString();
                            Console.WriteLine("eachNodeMoveCount: {0}", eachNodeMoveCount);
                            Console.WriteLine("totalMoveCount: {0}", totalMoveCount);

                            var invalidNodes = nodeHolder.GetCopyList().Where(n => !n.IsValid());
                            if (invalidNodes.Any())
                            {
                                Console.WriteLine("{0}-{1}\nSome nodes are invalid: {2}", algorithm, j, string.Join(",", invalidNodes.Select(n => n.Id.ToString())));
                            }

                            var topology = string.Join(", ", edgeHolder.GetCopyList().Select(e => e.ToString()));

                            var reportBuilder = new StringBuilder();

                            reportBuilder.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                                algorithm,
                                graphType,
                                nodeCount,
                                eachNodeMessageCount,
                                totalMessageCount,
                                eachNodeMoveCount,
                                totalMoveCount,
                                topology,
                                beforeInNodes,
                                beforeNInNodes,
                                afterInNodes,
                                afterNInNodes);

                            using (var streamWriter = new StreamWriter("test.txt", true))
                                streamWriter.WriteLine(reportBuilder.ToString());

                            Console.ReadKey();
                            //Console.Clear();
                        }
                    }
                }                              
            }
        }
    }
}