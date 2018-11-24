using AsyncSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UpdateBfsNode
{
    //public class _UpdateBfsNode : _Node
    //{
    //    /// <summary>
    //    /// Holds the parent of this node.
    //    /// </summary>
    //    public _Node Parent { get; set; }

    //    /// <summary>
    //    /// Holds a layer value for this node.
    //    /// </summary>
    //    public int MyLayer { get; set; }

    //    /// <summary>
    //    /// Holds the children as a list.
    //    /// </summary>
    //    public List<_Node> Children { get; set; }

    //    /// <summary>
    //    /// Holds the others as a list.
    //    /// </summary>
    //    public List<_Node> Others { get; set; }

    //    /// <summary>
    //    /// Initiates a node.
    //    /// </summary>
    //    /// <param name="id"></param>
    //    public _UpdateBfsNode ( int id )
    //        : base(id, null)
    //    {
    //        Children = new List<_Node>();
    //        Others = new List<_Node>();
    //        MyLayer = int.MaxValue;
    //    }

    //    /// <summary>
    //    /// Implementation of the _UpdateBfsNode algorithm lies in this method.
    //    /// </summary>
    //    /// <param name="receivedMessage"></param>
    //    protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage )
    //    {
    //        switch ( receivedMessage.MessageType )
    //        {
    //            case MessageTypes.Layer:
    //                {
    //                    var _layer = (int)receivedMessage.Data;
    //                    if ( MyLayer > _layer )
    //                    {
    //                        if ( Parent != null )
    //                        {
    //                            var edge = Visualizer.GetEdgeTo(Parent);
    //                            edge.Colorify(true);
    //                            edge.GetNode1().Visualizer.Draw(true);
    //                            edge.GetNode2().Visualizer.Draw(true);
    //                            Visualizer.VisualizeMessage(receivedMessage);
    //                            Visualizer.Log("n{0} is changing its parent from n{1} to n{2}", Id, Parent.Id, receivedMessage.Source.Id);
    //                        }
    //                        else
    //                        {
    //                            Visualizer.VisualizeMessage(receivedMessage);
    //                            Visualizer.Log("n{0} is setting its parent to n{1}", Id, receivedMessage.Source.Id);
    //                        }

    //                        Parent = receivedMessage.Source;
    //                        MyLayer = _layer;

    //                        var ack = new Message
    //                        {
    //                            Source = this,
    //                            Destination = receivedMessage.Source,
    //                            MessageType = MessageTypes.Ack
    //                        };
    //                        Underlying_Send(ack);

    //                        /// sending layer to to neighbours
    //                        foreach ( var neighbour in Neighbours.Where(n => n.Key != receivedMessage.Source.Id) )
    //                        {
    //                            Thread.Sleep(new Random().Next(100,900));
    //                            var layer = new Message
    //                            {
    //                                Data = _layer + 1,
    //                                Source = this,
    //                                Destination = neighbour.Value,
    //                                MessageType = MessageTypes.Layer
    //                            };
    //                            Underlying_Send(layer);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        //Log("n{0} already has a better parent:n{1} than n{2}", Id, Parent.Id, receivedMessage.Source.Id);
    //                        var reject = new Message
    //                        {
    //                            Source = this,
    //                            Destination = receivedMessage.Source,
    //                            MessageType = MessageTypes.Reject
    //                        };
    //                        Underlying_Send(reject);
    //                    }

    //                    break;
    //                }
    //            case MessageTypes.Ack:
    //                {
    //                    Visualizer.Log("n{0} received Ack from n{1}", Id, receivedMessage.Source.Id);
    //                    Children.Add(receivedMessage.Source);

    //                    break;
    //                }
    //            case MessageTypes.Reject:
    //                {
    //                    Visualizer.Log("n{0} received Reject from n{1}", Id, receivedMessage.Source.Id);
    //                    Others.Add(receivedMessage.Source);

    //                    break;
    //                }
    //        }
    //    }

    //    #region Initiators

    //    /// <summary>
    //    /// Since this algorithm is a single initiator method, this method is implemented.
    //    /// </summary>
    //    /// <param name="root"></param>
    //    public override void UserDefined_SingleInitiatorProcedure ( _Node root )
    //    {
    //        this.Underlying_Send(new AsyncSimulator.Message
    //        {
    //            Data = 1,
    //            Source = this,
    //            Destination = root,
    //            MessageType = MessageTypes.Layer
    //        });
    //    }

    //    protected override void UpdateNeighbourInformation(_Node neighbour)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion
    //}
}
