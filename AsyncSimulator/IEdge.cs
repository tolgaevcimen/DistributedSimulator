using System.Drawing;

namespace AsyncSimulator
{
    public interface IEdge
    {
        _Node GetNode1();
        _Node GetNode2();
        void Draw();
        bool OnPoint(Point location);
        void Colorify(bool reverted);
        void Delete(bool onlyEdgeDeleted);
    }
}
