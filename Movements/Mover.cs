using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Movements
{
    public class Mover : MonoBehaviour
    {
        public event EventHandler<bool> OnMovementChange;
        public event EventHandler<OnMoveEventArgs> OnMove;
        public class OnMoveEventArgs : EventArgs
        {
            public Vector3 direction;
            public float speedNormalized;
        }
        
        private NavMeshAgent agent;
        
        [SerializeField] private bool isMoving = false;
        
        public NavMeshAgent Agent => agent;
        public bool IsMoving => isMoving;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            GameManager.Instance.OnReset += GameManager_OnReset;
        }

        protected virtual void Update()
        {
            float speed = agent.velocity.magnitude;
            float maxSpeed = agent.speed; // Defina a velocidade máxima do agente
            float mappedSpeed = Mathf.Lerp(0, 1, speed / maxSpeed);

            
            OnMove?.Invoke(this, new OnMoveEventArgs
            {
                direction = agent.velocity.normalized,
                speedNormalized = mappedSpeed
            });
            
            switch (isMoving)
            {
                case false when mappedSpeed > 0.01f:
                    isMoving = true;
                    OnMovementChange?.Invoke(this, isMoving);
                    break;
                case true when mappedSpeed < 0.01f:
                    isMoving = false;
                    OnMovementChange?.Invoke(this, isMoving);
                    break;
            }
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            agent?.ResetPath();
        }
    }
}
