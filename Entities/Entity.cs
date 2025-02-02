using System;
using Animations;
using Behaviour;
using Managers;
using Movements;
using Sounds;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private string entityName;
        public string Name => entityName;
        
        protected AnimatorController animatorController;
        protected BehaviourTree behaviourTree;

        protected Mover mover;
        [SerializeField] private SoundPlayer soundPlayer;

        protected virtual void Awake()
        {
            animatorController = GetComponent<AnimatorController>();
            mover = GetComponent<Mover>();
        }

        protected virtual void Start()
        {
            mover.OnMovementChange += (sender, isMoving) =>
            {
                if (isMoving) soundPlayer.PlayLoop();
                else soundPlayer.Stop();
            };
            mover.OnMove += (sender, args) =>
            {
                animatorController.SetSpeed(args.speedNormalized);
            };
            
            GameManager.Instance.OnReset += GameManager_OnReset;
            GameManager.Instance.OnPause += GameManager_OnPause;
            soundPlayer?.Stop();
        }

        private void OnEnable()
        {
            soundPlayer?.Stop();
        }

        protected virtual void Update()
        {
            behaviourTree.Process();
        }
        
        private void GameManager_OnReset(object sender, EventArgs e)
        {
            Reset();
        }

        protected virtual void Reset()
        {
            soundPlayer?.Stop();
            behaviourTree?.Reset();
        }
        
        private void GameManager_OnPause(object sender, EventArgs e)
        {
            soundPlayer?.Stop();
        }
    }
}
