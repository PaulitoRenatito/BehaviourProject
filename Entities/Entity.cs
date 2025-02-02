using Animations;
using Behaviour;
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

        protected virtual void Start()
        {
            animatorController = GetComponent<AnimatorController>();
            mover = GetComponent<Mover>();
            
            mover.OnMovementChange += (sender, isMoving) =>
            {
                if (isMoving) soundPlayer.PlayLoop();
                else soundPlayer.Stop();
            };
            mover.OnMove += (sender, args) =>
            {
                animatorController.SetSpeed(args.speedNormalized);
            };
        }

        protected virtual void Update()
        {
            behaviourTree.Process();
        }
    }
}
