using UnityEngine;
using Utilities;

namespace Behaviour.Nodes
{
    public class Delay : Node
    {
        private readonly Timer timer;
        private Status status;
        
        private int counter = 0;
        
        public Delay(string name, int priority = 0, float time = 0) : base(name, priority)
        {
            this.timer = new CountdownTimer(time);
            
            timer.OnTimerStart += () =>
            {
                status = Status.Running;
                counter++;
                //Debug.Log($"Start: {name} - {counter}");
            };

            timer.OnTimerStop += () =>
            {
                Reset();
                status = children[0].Process();
                //Debug.Log($"Finished: {name} - {counter}");
            };
        }

        public override Status Process()
        {
            if (!timer.IsRunning)
            {
                timer.Start();
            }

            timer.Tick(Time.deltaTime);

            return status;
        }
    }
}
