using System;
using System.Collections.Generic;
using Behaviour.Blackboard;
using Sensor.SensorTypes;
using Tasks;
using Trees;
using UnityEngine;

namespace Entities
{
    public enum EntityTaskType
    {
        Static,
        Dynamic,
        Optimal
    }
    
    public class EntityTask : Entity
    {
        [Header("Tasks")]
        [SerializeField] Task fightTask;
        [SerializeField] Task manufactorTask;
        [SerializeField] Task plantTask;
        [SerializeField] Task healTask;
        [SerializeField] Task restTask;
        
        [Header("Type")]
        [SerializeField] private EntityTaskType type;
        
        private readonly BlackboardTask blackboardTask = new BlackboardTask();
        public BlackboardTask BlackboardTask => blackboardTask;

        protected override void Start()
        {
            base.Start();

            behaviourTree = type switch
            {
                EntityTaskType.Static => TreeFactory.GetStaticTreeTasks(mover.Agent,
                    blackboardTask,
                    new List<Task>()
                    {
                        fightTask,
                        manufactorTask,
                        plantTask,
                        healTask,
                        restTask
                    }),
                EntityTaskType.Dynamic => TreeFactory.GetDynamicTreeTasks(mover.Agent, blackboardTask, fightTask,
                    manufactorTask, plantTask, healTask, restTask),
                EntityTaskType.Optimal => TreeFactory.GetOptimalTreeTasks(mover.Agent, blackboardTask, fightTask,
                    manufactorTask, plantTask, healTask, restTask),
                _ => throw new Exception("Invalid EntityTaskType")
            };
        }

        protected override void Reset()
        {
            base.Reset();
            blackboardTask?.Reset();
        }
    }
}
