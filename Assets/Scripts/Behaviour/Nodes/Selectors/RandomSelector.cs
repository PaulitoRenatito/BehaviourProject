using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Behaviour.Nodes
{
    public class RandomSelector : PrioritySelector
    {
        protected override List<Node> SortChildren() => children.Shuffle().ToList();
        
        public RandomSelector(string name) : base(name) { }
    }
}