# DistributedSimulator

## Implementation of a Distributed Simulator in .NET C#

This paper includes some implementation details and explanations of a simulator framework which allows the users to run their distributed algorithms. The underlying mechanisms for message sending and receiving is provided by the libraries implemented in asynchronous manner. Users are supposed to implement and experiment their distributed algorithms by only writing their algorithms logic taking advantage of the underlying procedures. 

We will be providing the architecture of the framework, underlying processes, and moreover there will be 4 example implementations; which are: 
Flooding, 
FloodST(flooding spanning tree generation), 
UpdateBFS(asynchronous breadth first search tree generation), 
NeighDFS(asynchronous depth first search tree generation). 

Also there is a winforms project to visually represent the nodes, edges and the running of the algorithm.

## Overall Architecture
There exists 3 layers in the final looking of the environment.

First layer is the abstraction level which is the heart of the framework. There is a thread based implementation which allows nodes to send and receive messages simultaneously. In this layer we have yet implemented the simultaneous messaging mechanism in asynchronous manner - the algorithms lies in class _Node. Also in this layer lies a strategy pattern. In order to provide lower layer hooks to the visualization layers the classes in this layer we have an interface called IVisualizer.

Second layer is where the implementation of the user algorithms lie. By inheriting abstract node implementations users can create their own nodes behaving how they want when they receive a message. If the user wants to visualize an activity(coloring a node, an edge or printing some text on console) they should use the hook methods in IVisualizer interface.

And the third layer is where the nodes should be kicked to start. In this paper this layer is implemented in a winforms project where visualization of the nodes, edges and the message transitions takes place. But a user can omit it and implement it in any other .net compatible visual layer. 

The implementation of the strategy pattern lies here. Though strategy pattern allows many implementations for the sake of this paper we have only created one implementation, which is the VisualNode class. It implements the IVisualizer and does most of the hard work on the winforms project.

Additionally this layer has the factory pattern implementation in it, which provides to user to select the desired implementation and instantiate it by only giving an algorithm name.


### Implementation details

#### _Node:
The underlying template class for asynchronous nodes. This is an abstract class, therefore it needs to be implemented in derived classes, so that a concrete implementation appears.

    public abstract class _Node
    {
        /// <summary>
        /// Id of a node.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of neighbours of this node. Empty initially.
        /// </summary>
        public List<_Node> Neighbours { get; set; }

        /// <summary>
        /// A thread safe queue for received messages.
        /// </summary>
        protected ConcurrentQueue<Message> ReceiveQueue { get; set; }

        /// <summary>
        /// Flag for running state of the underlying thread.
        /// </summary>
        protected bool Running { get; set; }

        /// <summary>
        /// This is the hooker of the strategy pattern.
        /// </summary>
        public IVisualizer Visualizer { get; set; }

        /// <summary>
        /// As soon a node is created, the thread starts running.
        /// </summary>
        /// <param name="id"></param>
        public _Node ( int id ) { ... }
        
        /// <summary>
        /// This method will be implemented in sub classes for algorithm details.
        /// </summary>
        /// <param name="m"></param>
        protected abstract void UserDefined_ReceiveMessageProcedure ( Message m );

        #region initiator

        /// <summary>
        /// This method will be implemented for single initiation strategies.
        /// </summary>
        /// <param name="root"></param>
        public abstract void UserDefined_SingleInitiatorProcedure ( _Node root );

        /// <summary>
        /// This method will be implemented for concurrent initiation strategies.
        /// </summary>
        /// <param name="allNodes"></param>
        public abstract void UserDefined_ConcurrentInitiatorProcedure ( List<_Node> allNodes );

        #endregion

        #region Sender

        /// <summary>
        /// This method puts the given message to destinations ReceiveQueue
        /// </summary>
        /// <param name="m"></param>
        public void Underlying_Send ( Message m ) { ... }

        #endregion

        #region Receiver

        /// <summary>
        /// The implementation of the mechanism running in thread.
        /// </summary>
        void Receive () { ... }
        
        #endregion

        /// <summary>
        /// Starts the node
        /// </summary>
        public void Start() { ... }

        /// <summary>
        /// Stops the node
        /// </summary>
        public void Stop() { ... }
    }

--------------------------------------------------------------------------------------------------------------------
#### IVisualizer: 
This is the brain part of the strategy pattern. It has methods and properties for visualizing a node, and its interactions with other nodes.

    public interface IVisualizer
    {
        /// <summary>
        /// Location of this node.
        /// </summary>
        Point Location { get; set; }

        /// <summary>
        /// Checks whether the given point is on this node.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool OnIt ( Point p );

        /// <summary>
        /// Checks whether the given point is dangerously close to this node.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool Intersects ( Point p );

        /// <summary>
        /// Method for drawing the node.
        /// </summary>
      	/// <param name="changeColor">if set to true, the color of the node will change from default color to selected color.</param>
        void Draw ( bool changeColor = false );

        /// <summary>
        /// Method for showing a selected edge. Changes an edges color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="m"></param>
        void VisualizeMessage ( Message m );

        /// <summary>
        	/// Method for reverting a selected edge. Changes an edges color to unselected color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        void RevertEdgeBetween ( _Node n1, _Node n2 );

        /// <summary>
        /// Method for showing a text message.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="args"></param>
        void Log ( string l, params object[] args );
    }

--------------------------------------------------------------------------------------------------------------------
#### _FloodSTNode:
This class implements the Distributed Flooding Spanning Tree algorithm, straightforward.

    public class _FloodSTNode : _Node
    {
        /// <summary>
        /// Holds the parent of each node; initially, is set to null. 
        /// </summary>
        public _Node Parent { get; set; }

        /// <summary>
        /// Implementation of the FloodST.
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage ) {...}
    }

--------------------------------------------------------------------------------------------------------------------
#### _NeighDfsNode:
This class implements the Distributed Neighbour Discovery Depth First Search Tree generation algorithm.
 
    public class _NeighDfsNode : _Node
    {
        /// <summary>
        /// Holds a reference to the parent node.
        /// </summary>
        public _Node Parent { get; set; }

        /// <summary>
        /// Holds the list of id's of visited nodes.
        /// </summary>
        public List<int> VisitedNodes { get; set; }

        /// <summary>
        /// Randomizer for selecting an arbitrary node.
        /// </summary>
        public Random Randomizer { get; set; }

        /// <summary>
        /// Constructor for instantiating a _NeighDfsNode.
        /// </summary>
        /// <param name="id"></param>
        public _NeighDfsNode ( int id )
            : base(id)
        {...}

        /// <summary>
        /// The implementation of NeighDfsNode lies in this method.
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage ) {...}

        /// <summary>
        /// Since this algorithm is a single initiator method, this method is implemented.
        /// </summary>
        /// <param name="root"></param>
        public override void UserDefined_SingleInitiatorProcedure ( _Node root ) {...}

        /// <summary>
        /// Since this algorithm is a single initiator method, this method is NOT implemented intentionally.
        /// </summary>
        /// <param name="allNodes"></param>
        public override void UserDefined_ConcurrentInitiatorProcedure ( List<_Node> allNodes ) {...}
    }
--------------------------------------------------------------------------------------------------------------------
#### _UpdateBfsNode:
This class implements the Distributed Update Based Breadth First Search Tree generation algorithm.

    public class _UpdateBfsNode : _Node
    {
        /// <summary>
        /// Holds the parent of this node.
        /// </summary>
        public _Node Parent { get; set; }

        /// <summary>
        /// Holds a layer value for this node.
        /// </summary>
        public int MyLayer { get; set; }

        /// <summary>
        /// Holds the children as a list.
        /// </summary>
        public List<_Node> Children { get; set; }

        /// <summary>
        /// Holds the others as a list.
        /// </summary>
        public List<_Node> Others { get; set; }

        /// <summary>
        /// Initiates a node.
        /// </summary>
        /// <param name="id"></param>
        public _UpdateBfsNode ( int id )
            : base(id)
        {...}

        /// <summary>
        /// Implementation of the _UpdateBfsNode algorithm lies in this method.
        /// </summary>
        /// <param name="receivedMessage"></param>
        protected override void UserDefined_ReceiveMessageProcedure ( Message receivedMessage ) {...}

        #region Initiators

        /// <summary>
        /// Since this algorithm is a single initiator method, this method is implemented.
        /// </summary>
        /// <param name="root"></param>
        public override void UserDefined_SingleInitiatorProcedure ( _Node root ) {...}
        
        /// <summary>
        /// Since this algorithm is a single initiator method, this method is NOT implemented intentionally.
        /// </summary>
        /// <param name="allNodes"></param>
        public override void UserDefined_ConcurrentInitiatorProcedure ( List<_Node> allNodes ) {...}

        #endregion
    }

--------------------------------------------------------------------------------------------------------------------




