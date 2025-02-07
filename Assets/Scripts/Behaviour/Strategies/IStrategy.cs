using Behaviour.Nodes;

namespace Behaviour.Strategies
{
    public interface IStrategy
    {
        Node.Status Process();

        void Reset()
        {
            // Noop
        }
    }
}
