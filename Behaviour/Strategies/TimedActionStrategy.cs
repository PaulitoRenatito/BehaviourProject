using System;
using Behaviour.Nodes;
using UnityEngine;
using Utilities;

namespace Behaviour.Strategies
{
    public class TimedActionStrategy : IStrategy
    {
        private readonly Action action;
        private readonly Timer timer;

        private Node.Status status;

        public TimedActionStrategy(Action action, Timer timer)
        {
            this.action = action;
            this.timer = timer;
        }

        public Node.Status Process()
        {
            if (!timer.IsRunning)
            {
                timer.Start();
                timer.OnTimerStart += () =>
                {
                    action();
                    status = Node.Status.Success;
                };
                
                status = Node.Status.Running;
            }
            
            timer.Tick(Time.deltaTime);
            
            return status;
        }
    }
}
