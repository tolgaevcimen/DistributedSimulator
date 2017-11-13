﻿using AsyncSimulator;
using ConsoleEnvironment.GraphGenerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEnvironment
{
    public class RunReport
    {
        public AlgorithmType AlgorithmType { get; set; }
        public GraphType GraphType { get; set; }
        public int NodeCount { get; set; }

        public List<Tuple<int, int>> Topology { get; set; }

        public List<int> BeforeInNodes { get; set; }
        public List<int> BeforeNInNodes { get; set; }
        public List<int> AfterInNodes { get; set; }
        public List<int> AfterNInNodes { get; set; }
        public List<int> InvalidNodes { get; set; }

        public Dictionary<int, int> EachNodesMessageCount { get; set; }
        public int TotalMessageCount { get; set; }
        public Dictionary<int, int> EachNodesMoveCount { get; set; }
        public int TotalMoveCount { get; set; }

        public int MaxDegree { get; set; }
        public double AvgDegree { get; set; }
        public int MinDegree { get; set; }

        public RunReport(AlgorithmType algorithmType, GraphType graphType)
        {
            AlgorithmType = algorithmType;
            GraphType = graphType;
        }

        public void ReportTopology(List<IEdge> edges)
        {
            Topology = edges.Select(e => Tuple.Create(e.GetNode1().Id, e.GetNode2().Id)).ToList();
        }

        public void ReportNodes(List<_Node> nodes, bool before)
        {
            if (before)
            {
                BeforeInNodes = nodes.Where(n => n.Selected()).Select(n => n.Id).ToList();
                BeforeNInNodes = nodes.Where(n => !n.Selected()).Select(n => n.Id).ToList();
                //Console.WriteLine("before in nodes:\t{0}", beforeInNodes);
                //Console.WriteLine("before n-in nodes:\t{0}", beforeNInNodes);
            }
            else
            {
                AfterInNodes = nodes.Where(n => n.Selected()).Select(n => n.Id).ToList();
                AfterNInNodes = nodes.Where(n => !n.Selected()).Select(n => n.Id).ToList();
                //Console.WriteLine("after in nodes:\t{0}", afterInNodes);
                //Console.WriteLine("after n-in nodes:\t{0}", afterNInNodes);
            }
        }

        public void GatherMessageCount(List<_Node> nodes)
        {
            EachNodesMessageCount = nodes.ToDictionary(n => n.Id, n => n.MessageCount);
            TotalMessageCount = nodes.Sum(n => n.MessageCount);

            //Console.WriteLine("eachNodeMessageCount: {0}", eachNodeMessageCount);
            //Console.WriteLine("totalMessageCount: {0}", totalMessageCount);
        }

        public void GatherMoveCount(List<_Node> nodes)
        {
            EachNodesMoveCount = nodes.ToDictionary(n => n.Id, n => n.MoveCount);
            TotalMoveCount = nodes.Sum(n => n.MoveCount);

            //Console.WriteLine("eachNodeMoveCount: {0}", eachNodeMoveCount);
            //Console.WriteLine("totalMoveCount: {0}", totalMoveCount);
        }

        public void ReportInvalidNodes(List<_Node> nodes)
        {
            InvalidNodes = nodes.Where(n => !n.IsValid()).Select(n => n.Id).ToList();
            //if (invalidNodes.Any())
            //{
            //    Console.WriteLine("{0}-{1}\nSome nodes are invalid: {2}", algorithmType, string.Join(",", invalidNodes.Select(n => n.Id.ToString())));
            //}
        }

        public void ReportDegrees(List<_Node> nodes)
        {
            MinDegree = nodes.Min(n => n.Neighbours.Count);
            AvgDegree = nodes.Average(n => n.Neighbours.Count);
            MaxDegree = nodes.Max(n => n.Neighbours.Count);

            //Console.WriteLine("minDegree: {0}, avarageDegree: {1}, maxDegree: {2}", minDegree, avarageDegree, maxDegree);
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}",
                AlgorithmType,
                GraphType,
                NodeCount,
                string.Join(", ", EachNodesMessageCount.Select(kv => string.Format("{0}: {1}", kv.Key, kv.Value))),
                TotalMessageCount,
                string.Join(", ", EachNodesMoveCount.Select(kv => string.Format("{0}: {1}", kv.Key, kv.Value))),
                TotalMoveCount,
                string.Join(", ", Topology.Select(t => string.Format("({0}-{1})", t.Item1, t.Item2))),
                string.Join(", ", BeforeInNodes),
                string.Join(", ", BeforeNInNodes),
                string.Join(", ", AfterInNodes),
                string.Join(", ", AfterNInNodes),
                MinDegree + "|" + AvgDegree + "|" + MaxDegree);
        }
    }
}
