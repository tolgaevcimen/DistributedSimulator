using System.Drawing;

namespace AsyncSimulator
{
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
        bool OnIt(Point p);

        /// <summary>
        /// Checks whether the given point is dangerously close to this node.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool Intersects(Point p);

        /// <summary>
        /// Method for drawing the node.
        /// </summary>
        /// <param name="changeColor">if set to true, the color of the node will change from default color to selected color.</param>
        void Draw(bool changeColor = false);

        /// <summary>
        /// Method for showing a selected edge. Changes an edges color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="m"></param>
        void VisualizeMessage(Message m);

        /// <summary>
        /// Method for reverting a selected edge. Changes an edges color to unselected color lying between two nodes that the message is sourced from and destined to.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        void RevertEdgeBetween(_Node n1, _Node n2);

        /// <summary>
        /// Method for showing a text message.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="args"></param>
        void Log(string l, params object[] args);
    }
}
