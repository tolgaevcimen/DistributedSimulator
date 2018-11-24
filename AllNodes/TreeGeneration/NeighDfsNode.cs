using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NeighDfsNode
{
    //public class _NeighDfsNode : _Node
    //{
    //    /// <summary>
    //    /// Holds a reference to the parent node.
    //    /// </summary>
    //    public _Node Parent { get; set; }

    //    /// <summary>
    //    /// Holds the list of id's of visited nodes.
    //    /// </summary>
    //    public List<int> VisitedNodes { get; set; }

    //    /// <summary>
    //    /// Randomizer for selecting an arbitrary node.
    //    /// </summary>
    //    public Random Randomizer { get; set; }

    //    /// <summary>
    //    /// Constructor for instantiating a _NeighDfsNode.
    //    /// </summary>
    //    /// <param name="id"></param>
    //    public _NeighDfsNode ( int id )
    //        : base(id, null)
    //    {
    //        VisitedNodes = new List<int>();
    //        Randomizer = new Random();
    //    }

    //    /// <summary>
    //    /// The implementation of NeighDfsNode lies in this method.
    //    /// </summary>
    //    /// <param name="receivedMessage"></param>
    //    protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage )
    //    {
    //        if ( receivedMessage.MessageType != MessageTypes.Token ) return;

    //        /// token received first time
    //        if ( Parent == null )
    //        {
    //            Parent = receivedMessage.Source;
    //            Visualizer.VisualizeMessage(receivedMessage);
    //            Visualizer.Log("n{0} is setting its parent to n{1}", Id, receivedMessage.Source.Id);
    //        }

    //        VisitedNodes.AddRange((List<int>)receivedMessage.Data);
    //        var unvisitedNodes = Neighbours.Select(n => n.Key).Except(VisitedNodes);

    //        Thread.Sleep(500);

    //        /// choose an unsearched node if any
    //        if ( unvisitedNodes.Any() )
    //        {
    //            var randomNodeId = unvisitedNodes.ElementAt(Randomizer.Next(0, unvisitedNodes.Count()));
    //            var randomNode = Neighbours.First(n => n.Key == randomNodeId);
                
    //            var vislist = VisitedNodes.ToList();
    //            vislist.Add(Id);

    //            var token = new Message
    //            {
    //                Source = this,
    //                Destination = randomNode.Value,
    //                Data = vislist,
    //                MessageType = MessageTypes.Token
    //            };

    //            this.Underlying_Send(token);
    //        }

    //        /// if all nodes are searched, and this node is root terminate
    //        else if ( Id == 0 )
    //        {
    //            Visualizer.Log("token is back at root", Id, receivedMessage.Source.Id);
    //        }

    //        /// if all nodes are searched, and this node is NOT root, return token to parent
    //        else
    //        {
    //            Visualizer.Log("n{0} couldn't find an unsearched node", Id, receivedMessage.Source.Id);

    //            var vislist = VisitedNodes.ToList();
    //            vislist.Add(Id);

    //            var token = new Message
    //            {
    //                Source = this,
    //                Destination = Parent,
    //                Data = vislist,
    //                MessageType = MessageTypes.Token
    //            };

    //            this.Underlying_Send(token);
    //        }
    //    }

    //    /// <summary>
    //    /// Since this algorithm is a single initiator method, this method is implemented.
    //    /// </summary>
    //    /// <param name="root"></param>
    //    public override void UserDefined_SingleInitiatorProcedure ( _Node root )
    //    {
    //        this.Underlying_Send(new Message
    //        {
    //            Source = this,
    //            Destination = root,
    //            Data = new List<int>(),
    //            MessageType = MessageTypes.Token
    //        });
    //    }

    //    protected override void UpdateNeighbourInformation(_Node neighbour)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
