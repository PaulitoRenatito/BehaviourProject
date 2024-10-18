using System.Collections.Generic;
using System.Linq;

namespace Behaviour.Nodes
{
    public class PrioritySelector : Selector
    {
        List<Node> sortedChildren;
        List<Node> SortedChildren => sortedChildren ??= SortChildren();
        
        protected virtual List<Node> SortChildren() => children.OrderByDescending(child => child.priority).ToList();
        
        public PrioritySelector(string name) : base(name) { }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    default:
                        continue;
                }
            }
            
            return Status.Failure;
        }

        public override void Reset()
        {
            base.Reset();
            sortedChildren = null;
        }
    }
}