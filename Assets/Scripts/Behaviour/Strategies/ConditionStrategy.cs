using System;
using Behaviour.Nodes;

namespace Behaviour.Strategies
{
    public class ConditionStrategy : IStrategy
    {
        readonly Func<bool> predicate;

        public ConditionStrategy(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public Node.Status Process() => predicate() ? Node.Status.Success : Node.Status.Failure;
    }
}
