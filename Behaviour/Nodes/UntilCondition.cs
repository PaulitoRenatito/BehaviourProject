using System;
using UnityEngine;
using Utilities;

namespace Behaviour.Nodes
{
    public class UntilCondition : Node
    {
        readonly Func<bool> predicate;
        
        public UntilCondition(string name, Func<bool> predicate) : base(name)
        {
            this.predicate = predicate;
        }

        public override Status Process()
        {
            if (!predicate.Invoke())
            {
                children[0].Process();
                return Status.Running;
            }
            
            Reset();
            return Status.Success;
        }
    }
}
