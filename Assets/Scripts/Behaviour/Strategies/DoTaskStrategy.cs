using System;
using Behaviour.Nodes;
using Tasks;
using UnityEngine;
using Utilities;

namespace Behaviour.Strategies
{
    public class DoTaskStrategy : IStrategy
    {
        private readonly Task task;
        private readonly Action onCompleteAction;
        private Timer timer;

        private Node.Status status = Node.Status.Running;

        public DoTaskStrategy(Task task, Action onCompleteAction = null)
        {
            this.task = task;
            this.onCompleteAction = onCompleteAction;
            InitializeTimer();
        }

        public Node.Status Process()
        {
            if (timer.IsRunning)
            {
                timer.Tick(Time.deltaTime);
                return status;
            }

            if (status == Node.Status.Success) return status;

            timer.Start();

            return status;
        }

        public void Reset()
        {
            status = Node.Status.Running;
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new CountdownTimer(task.TimeToComplete);
            timer.OnTimerStart += () =>
            {
                status = Node.Status.Running;
                task.StartTask();
            };
            timer.OnTimerStop += () =>
            {
                onCompleteAction?.Invoke();

                status = Node.Status.Success;
            };
        }
    }
}
