namespace AsyncSimulator
{
    public class MessageEx
    {
        public MessagePassingNode Source { get; set; }
        public MessagePassingNode Destination { get; set; }
        public MessageTypes MessageType { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            return string.Format("SId: {0}, DId: {1}", Source != null ? Source.Id : -1, Destination != null ? Destination.Id : -1);
        }
    }

    public class Message
    {
        public _Node Source { get; set; }
        //public _Node Destination { get; set; }
        public int DestinationId { get; set; }
        public MessageTypes MessageType { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            return string.Format("SId: {0}, DId: {1}", Source != null ? Source.Id : -1, DestinationId);
            //return string.Format("SId: {0}, DId: {1}", Source != null ? Source.Id : -1, Destination != null ? Destination.Id : -1);
        }
    }
}
