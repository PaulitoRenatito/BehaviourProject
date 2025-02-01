using Behaviour.Blackboard;
using Behaviour.Nodes;
using Tasks;
using UnityEngine;
using Utilities;

namespace Behaviour.Strategies
{
    public class DoTaskStrategy : IStrategy
    {
        private readonly Task task;
        private readonly BlackboardTask blackboardTask;
        private Timer timer;

        private Node.Status status = Node.Status.Running;

        public DoTaskStrategy(Task task, BlackboardTask blackboardTask = null)
        {
            this.task = task;
            if (blackboardTask != null) this.blackboardTask = blackboardTask;
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
                if (blackboardTask != null)
                {
                    blackboardTask.Health += task.TaskSo.Health;
                    blackboardTask.Stamina += task.TaskSo.Stamina;
                    blackboardTask.Profit += task.TaskSo.Profit;
                    blackboardTask.ElapsedTime += task.TimeToComplete;
                    
                    Debug.Log(
                        $"Task: {task.Name} | Lucro: {blackboardTask.Profit} | Vida: {blackboardTask.Health} | Stamina: {blackboardTask.Stamina} | Tempo: {blackboardTask.ElapsedTime}");
                }

                status = Node.Status.Success;
            };
        }
    }
}
