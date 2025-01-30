using Behaviour.Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour.Strategies
{
    public class GotoDestinationStrategy : IStrategy
    {
        readonly NavMeshAgent agent;
        readonly Transform destination;
        
        bool isPathCalculated;

        private readonly bool debug = false;
        
        public GotoDestinationStrategy(NavMeshAgent agent, Transform destination)
        {
            this.agent = agent;
            this.destination = destination;
        }
        
        public Node.Status Process()
        {
            if (!isPathCalculated)
            {
                if (debug) Debug.Log("Calculating path to " + destination.name);
                agent.SetDestination(destination.position);
                isPathCalculated = true;
            }
            
            if (debug) Debug.Log("Remaining distance: " + agent.remainingDistance);
            
            if (agent.pathPending) return Node.Status.Running;
            
            if (agent.remainingDistance > 0.1f) return Node.Status.Running;
            
            if (debug) Debug.Log("Arrived at " + destination.name);
            
            isPathCalculated = false;
            
            return Node.Status.Success;
        }
    }
}
