using Animations;
using Behaviour;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class Entity : MonoBehaviour
    {

        protected NavMeshAgent agent;
        protected AnimatorController animatorController;
        protected BehaviourTree behaviourTree;

        protected virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorController = GetComponent<AnimatorController>();
        }

        protected virtual void Update()
        {
            animatorController.SetSpeed(agent.velocity.magnitude);
            behaviourTree.Process();
        }
    }
}
