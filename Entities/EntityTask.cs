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
        Dynamic
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
        
        [Header("Debug")]
        [SerializeField] private Task currentTask;
        
        private readonly BlackboardTask blackboardTask = new BlackboardTask();
        
        private SensorTask sensorTask;

        protected override void Start()
        {
            base.Start();
            
            sensorTask = GetComponent<SensorTask>();

            sensorTask.OnEnterSensor += SensorTask_OnEnterSensor;
            sensorTask.OnExitSensor += SensorTask_OnExitSensor;

            behaviourTree = type switch
            {
                EntityTaskType.Static => TreeFactory.GetStaticTreeTasks(agent,
                    new List<Task>()
                    {
                        fightTask,
                        manufactorTask,
                        plantTask,
                        healTask,
                        restTask
                    }),
                EntityTaskType.Dynamic => TreeFactory.GetDynamicTreeTasks(agent, blackboardTask, fightTask,
                    manufactorTask, plantTask, healTask, restTask),
                _ => throw new Exception("Invalid EntityTaskType")
            };
        }
        
        private void SensorTask_OnEnterSensor(object sender, Task e)
        {
            currentTask = e;
        }

        private void SensorTask_OnExitSensor(object sender, Task e)
        {
            currentTask = null;
        }
    }
}
