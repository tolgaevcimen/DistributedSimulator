using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloodingNode;

namespace ASyncSimulator1
{
    class Program
    {
        static void Main ( string[] args )
        {
            var nodeX = new _FloodingNode(0);

            var nodeA = new _FloodingNode(1);

            var nodeB = new _FloodingNode(2);

            var nodeC = new _FloodingNode(3);

            var nodeD = new _FloodingNode(4);

            nodeA.Neighbours.Add(nodeB);
            nodeA.Neighbours.Add(nodeC);
            nodeA.Neighbours.Add(nodeD);

            nodeX.Underlying_Send(new Message
            {
                Data = nodeX.Id + "-" + nodeA.Id,
                Source = nodeX,
                Destination = nodeA,
                MessageType = 0
            });

            Console.ReadLine();
        }
    }
}
