using UnityEngine;

namespace Behaviour.Nodes
{
    public class Repeat : Node
    {
        private int repeatCount;
        private int currentCount;
        private bool reset = false;

        public Repeat(string name, int repeatCount, bool reset = false) : base(name)
        {
            this.repeatCount = repeatCount;
            currentCount = 0;
            this.reset = reset;
        }

        public override Status Process()
        {
            if (currentCount >= repeatCount)
            {
                Debug.Log($"Reset: {reset} | Repeat Count: {repeatCount} | Current Count: {currentCount}");
                if (reset) Reset();
                return Status.Failure;
            }
            
            Status childStatus = children[0].Process();
            
            if (childStatus is Status.Success or Status.Failure)
            {
                currentCount++;
            }

            return Status.Running;
        }

        public override void Reset()
        {
            currentCount = 0;
        }
    }
}