﻿using AsyncSimulator;

namespace VisualInterface.GraphGenerator
{
    internal interface IGraphGenerator
    {
        void Generate(int nodeCount, NodeHolder nodeHolder, EdgeHolder edgeHolder, string SelectedAlgorithm);
    }
}
