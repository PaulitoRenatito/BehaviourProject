using Animations;
using Behaviour;
using Behaviour.Nodes;
using Behaviour.Strategies;
using Sensor.SensorTypes;
using Tasks;
using Trees;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] Task fightTask;
        [SerializeField] Task manufactorTask;
        [SerializeField] Task plantTask;
        [SerializeField] Task healTask;
        [SerializeField] Task restTask;
        
        private NavMeshAgent agent;
        private AnimatorController animatorController;
        private BehaviourTree behaviourTree;
        private SensorTask sensorTask;
        
        [SerializeField] private Task currentTask;
        
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorController = GetComponent<AnimatorController>();
            sensorTask = GetComponent<SensorTask>();
            
            sensorTask.OnEnterSensor += SensorTask_OnEnterSensor;
            sensorTask.OnExitSensor += SensorTask_OnExitSensor;
            
            behaviourTree = TreeTasks.GetTreeTasks(agent, fightTask, manufactorTask, plantTask, healTask, restTask);
        }

        private void SensorTask_OnEnterSensor(object sender, Task e)
        {
            currentTask = e;
        }
        
        private void SensorTask_OnExitSensor(object sender, Task e)
        {
            currentTask = null;
        }

        private void Update()
        {
            animatorController.SetSpeed(agent.velocity.magnitude);
            behaviourTree.Process();
        }
    }
}
