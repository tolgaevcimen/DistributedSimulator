namespace FloodSTNode
{
    //public class _FloodSTNode : _Node
    //{
    //    public _Node Parent { get; set; }

    //    public _FloodSTNode ( int id )
    //        : base(id, null)
    //    {

    //    }

    //    protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage )
    //    {
    //        var now = DateTime.Now;

    //        Visualizer.Log("node{0} received:( {1} )", this.Id, receivedMessage, receivedMessage.Source.Id, now.Minute + ":" + now.Second);
    //        Thread.Sleep(1000);

    //        if ( Parent != null )
    //        {
    //            Visualizer.Log("---I'm {0}, I already have a parent: {1}", Id, Parent.Id);
    //            return;
    //        }
    //        Parent = receivedMessage.Source;

    //        foreach ( var neighbour in Neighbours )
    //        {
    //            var m = new Message
    //            {
    //                Data = this.Id + "-" + neighbour.Key,
    //                Source = this,
    //                Destination = neighbour.Value,
    //                MessageType = 0
    //            };
    //            this.Underlying_Send(m);
    //        }

    //        Visualizer.VisualizeMessage(receivedMessage);

    //        Visualizer.Log("receiver {0} done", this.Id);
    //    }

    //    public override void UserDefined_SingleInitiatorProcedure ( _Node root )
    //    {
    //        this.Underlying_Send(new AsyncSimulator.Message
    //        {
    //            Data = this.Id + "-" + root.Id,
    //            Source = this,
    //            Destination = root,
    //            MessageType = 0
    //        });
    //    }

    //    protected override void UpdateNeighbourInformation(_Node neighbour)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
