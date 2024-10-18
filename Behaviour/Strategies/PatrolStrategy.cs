using System.Collections.Generic;
using Behaviour.Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour.Strategies
{
    public class PatrolStrategy : IStrategy
    {
        private readonly Transform entity;
        readonly NavMeshAgent agent;
        readonly List<Transform> patrolPoints;
        readonly float patrolSpeed;
        int currentIndex; 
        bool isPathCalculated;

        public PatrolStrategy(Transform entity, NavMeshAgent agent, List<Transform> patrolPoints, float patrolSpeed = 2f)
        {
            this.entity = entity;
            this.agent = agent;
            this.patrolPoints = patrolPoints;
            this.patrolSpeed = patrolSpeed;
        }

        public Node.Status Process()
        {
            if (currentIndex == patrolPoints.Count) return Node.Status.Success;
            
            Transform target = patrolPoints[currentIndex];
            agent.SetDestination(target.position);
            entity.LookAt(target);

            if (isPathCalculated && agent.remainingDistance < 0.1f)
            {
                currentIndex++;
                isPathCalculated = false;
            }

            if (agent.pathPending)
            {
                isPathCalculated = true;
            }

            return Node.Status.Running;
        }

        public void Reset() => currentIndex = 0;
    }
}
