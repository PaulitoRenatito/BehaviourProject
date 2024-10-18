namespace Behaviour.Nodes
{
    public class Repeat : Node
    {
        private int repeatCount;
        private int currentCount;

        public Repeat(string name, int repeatCount) : base(name)
        {
            this.repeatCount = repeatCount;
            currentCount = 0;
        }

        public override Status Process()
        {
            if (currentCount >= repeatCount)
            {
                Reset();
                return Status.Success;
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